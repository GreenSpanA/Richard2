﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Richard2.Models;
using Richard2.Repository;

namespace Richard2.Controllers
{
    public class sampleMenuController : Controller
    {
        private readonly sampleMenuRepository sMenuRepository;

        //new
        private readonly currentFileRepository sFileRepository;     


        public sampleMenuController(IConfiguration configuration)
        {
            sMenuRepository = new sampleMenuRepository(configuration);
            //new
            sFileRepository = new currentFileRepository(configuration);
        }




        public IActionResult Index()
        {
            return View(sMenuRepository.FindAll());
        }

       
        public IActionResult ViewCreate()
        {
            return PartialView("_Create");
        }

        // POST: Customer/Create
        [HttpPost]
        public IActionResult Create(sampleMenu cust)
        {
            if (ModelState.IsValid)
            {
                sMenuRepository.Add(cust);
                return RedirectToAction("Index");
            }
            return View(cust);

        }
        
        public IActionResult ViewEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            sampleMenu obj = sMenuRepository.FindByID(id.Value);

            if (obj == null)
            {
                return NotFound();
            }            
            return PartialView("_Edit", obj);
        }   

        [HttpPost]
        public IActionResult ViewEdit(sampleMenu obj)
        {

            if (ModelState.IsValid)
            {
                sMenuRepository.Update(obj);
                return RedirectToAction("Index");
            }
                      
            return View(obj);
        }

        // GET:/Customer/Delete/1
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            sMenuRepository.Remove(id.Value);
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public IActionResult InfoMenu()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult InfoMenu(int? id)
        {
            id = 0;
            sampleMenu obj = sMenuRepository.FindByFile(id.Value);

            if (obj == null)
            {
                return NotFound();
            }           

            return Json(new { data = obj });
        }

        [HttpPost]
        public IActionResult InfoMenu(string menu_id)
        {
            string info = $"Info: {menu_id}";
            return Content(info);
        }

        //[HttpPost]
        //public ActionResult Subscribe(currentFile model_f)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        currentFile obj = sFileRepository.FindByID(1);
        //        sFileRepository.Update(obj);
        //    }

        //    return View("Index", model_f);
        //}

    }
}
