using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Richard2.Models;
using System.IO;

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

        
        public ActionResult SomeActionMethod()
        {
            //var employees = DataRepository.GetEmployees();

            ////"Business logic" methog that filter employees by the employer id
            //var companyEmployees = (from e in employees
            //                        where (CompanyID == null || e.CompanyID == CompanyID)
            //                        select e).ToList();

            ////UI processing logic that filter company employees by name and paginates them
            //var filteredEmployees = (from e in companyEmployees
            //                         where (param.sSearch == null || e.Name.ToLower().Contains(param.sSearch.ToLower()))
            //                         select e).ToList();
            //var result = from emp in filteredEmployees.Skip(param.iDisplayStart).Take(param.iDisplayLength)
            //             select new[] { Convert.ToString(emp.EmployeeID), emp.Name, emp.Position };

            //var companies = DataRepository.GetCompanies();
            var companies = System.IO.File.ReadAllLines("Models\\Data\\company_list.csv")
                                           .Skip(1)
                                           .Select(v => new Company(v))
                                           .ToList();

            //var result = companies;
            var result = from emp in companies select new[] {Convert.ToString(emp.ID), Convert.ToString(emp.Name), emp.Address, emp.Town};
            return Json(new
            {
              
                iTotalRecords = companies.Count,
                iTotalDisplayRecords = companies.Count,
                aaData = result
            });
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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

            var employees = DataRepository.GetEmployees();

            //"Business logic" methog that filter employees by the employer id
            var companyEmployees = (from e in employees
                                    where (CompanyID == null || e.CompanyID == CompanyID)
                                    select e).ToList();

            //UI processing logic that filter company employees by name and paginates them
            var filteredEmployees = (from e in companyEmployees
                                     where (param.sSearch == null || e.Name.ToLower().Contains(param.sSearch.ToLower()))
                                     select e).ToList();
            var result = from emp in filteredEmployees.Skip(param.iDisplayStart).Take(param.iDisplayLength)
                         select new[] { Convert.ToString(emp.EmployeeID), emp.Name, emp.Position };

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

