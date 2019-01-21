using System;
//using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Richard2.Models;
//using System.IO;

namespace Richard2.Controllers
{
    public class HomeController : Controller
    {
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

