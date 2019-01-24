using System;
//using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Richard2.Models;
//using System.IO;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;

namespace Richard2.Controllers
{
    public class HomeController : Controller
    {
        //21.01
        private readonly IFileProvider fileProvider;

        public HomeController(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }


        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            //var path = Path.Combine(
            //            Directory.GetCurrentDirectory(), "wwwroot",
            //            file.GetFilename());
            var path = Path.Combine(
                      Directory.GetCurrentDirectory(), "wwwroot/PDF",
                      "menu.pdf");


            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            //return RedirectToAction("Files");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileViaModel(FileInputModel model)
        {
            if (model == null ||
                model.FileToUpload == null || model.FileToUpload.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot//PDF",  
                        model.FileToUpload.GetFilename());

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.FileToUpload.CopyToAsync(stream);
            }

            //return RedirectToAction("Files");
            return RedirectToAction("Index");
        }


        public IActionResult Files()
        {
            var model = new FilesViewModel();
            foreach (var item in this.fileProvider.GetDirectoryContents(""))
            {
                model.Files.Add(
                    new FileDetails { Name = item.Name, Path = item.PhysicalPath });
            }
            return View(model);
        }

        public FileResult GetReport()
        {
            string ReportURL = "wwwroot\\PDF\\menu.pdf";
            byte[] FileBytes = System.IO.File.ReadAllBytes(ReportURL);
            return File(FileBytes, "application/pdf");
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot/PDF", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }


        //
        public IActionResult Index()
        {  
           return View();
           
        }        

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }        

        public ActionResult RestaurantsHandler()
        {           
            var companies = System.IO.File.ReadAllLines("Models\\Data\\restaurants_list.csv")
                                           .Skip(1)
                                           .Select(v => new Restaurants(v))
                                           .ToList();

            //var result = companies;
            var result = from emp in companies select new[] {Convert.ToString(emp.ID), Convert.ToString(emp.Name), Convert.ToString(emp.Website),
                                                            Convert.ToString(emp.Neighborhood)};
            return Json(new
            {
              
                iTotalRecords = companies.Count,
                iTotalDisplayRecords = companies.Count,
                aaData = result
            });
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "If you have any question...";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult MasterDetailsAjaxHandler(JQueryDataTableParamModel param, int? CompanyID)
        {           

            var employees = System.IO.File.ReadAllLines("Models\\Data\\menus.csv")
                                           .Skip(1)
                                           .Select(v => new Menus(v))
                                           .ToList();
            //"Business logic" methog that filter menu items by the rest id
            var companyEmployees = (from e in employees
                                        //where (CompanyID == null || e.RestID == CompanyID) 
                                    where (e.RestID == CompanyID)
                                    select e).ToList();

            //UI processing logic that filter company employees by name and paginates them
            var filteredEmployees = (from e in companyEmployees
                                     where (param.sSearch == null || e.RestaurantName.ToLower().Contains(param.sSearch.ToLower()))
                                     select e).ToList();
            var result = from emp in filteredEmployees.Skip(param.iDisplayStart).Take(param.iDisplayLength)
                         select new[] {emp.ItemName, emp.Description, emp.VegComment, Convert.ToString(emp.Price),
                         emp.Category, emp.Comment, emp.SizeComment, emp.PriceComment,
                         emp.MenuLink, emp.MenuType, Convert.ToString(emp.RestID), emp.UpdateTime};

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = companyEmployees.Count,
                iTotalDisplayRecords = filteredEmployees.Count,
                aaData = result
            });
        }  


    }
}

