using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Richard2.Models;
using Richard2.Repository;

namespace Richard2.Controllers
{
    public class sampleMenuController : Controller
    {
        private readonly sampleMenuRepository sMenuRepository;

        public sampleMenuController(IConfiguration configuration)
        {
            sMenuRepository = new sampleMenuRepository(configuration);
        }


        public IActionResult Index()
        {
            return View(sMenuRepository.FindAll());
        }

        public IActionResult Create()
        {
            return View();
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

        // GET: /Customer/Edit/1
        public IActionResult Edit(int? id)
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
            return View(obj);

        }

        [HttpPost]
        public IActionResult Edit(sampleMenu obj)
        {

            if (ModelState.IsValid)
            {
                sMenuRepository.Update(obj);
                return RedirectToAction("Index");
            }
            //return View(obj);           
            return PartialView(obj);
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
    }
}
