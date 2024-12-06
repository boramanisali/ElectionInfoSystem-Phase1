using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services.Bases;
using BLL.Models;
using BLL.DAL;
using BLL.Services;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class PoliticalPartiesController : MvcController
    {
        // Service injections:
        private readonly IPoliticalPartiesService _politicalPartiesService;

        /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
        //private readonly IService<{Entity}, {Entity}Model> _{Entity}Service;

        public PoliticalPartiesController(
            IPoliticalPartiesService politicalPartiesService

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //, Service<{Entity}, {Entity}Model> {Entity}Service
        )
        {
            _politicalPartiesService = politicalPartiesService;

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //_{Entity}Service = {Entity}Service;
        }

        // GET: PoliticalParties
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _politicalPartiesService.Query().ToList();
            return View(list);
        }

        // GET: PoliticalParties/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _politicalPartiesService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            
            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //ViewBag.{Entity}Ids = new MultiSelectList(_{Entity}Service.Query().ToList(), "Record.Id", "Name");
        }

        // GET: PoliticalParties/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: PoliticalParties/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PoliticalPartiesModel politicalParties)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _politicalPartiesService.Create(politicalParties.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = politicalParties.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(politicalParties);
        }

        // GET: PoliticalParties/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _politicalPartiesService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: PoliticalParties/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PoliticalPartiesModel politicalParties)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _politicalPartiesService.Update(politicalParties.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = politicalParties.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(politicalParties);
        }

        // GET: PoliticalParties/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _politicalPartiesService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: PoliticalParties/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _politicalPartiesService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
