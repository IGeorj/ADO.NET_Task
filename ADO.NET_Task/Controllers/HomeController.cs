using ADO.NET_Task.Models;
using ADO.NET_Task.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADO.NET_Task.Controllers
{
    public class HomeController : Controller
    {
        private readonly LocationRepository repository = new LocationRepository();
        
        [HttpGet]
        public ActionResult Index()
        {
            var locations = repository.GetLocations(1, 1, 5);
            return View(locations);
        }

        [HttpGet]
        public ActionResult GetLocations(int subscriberId, int page, int pageSize)
        {
            var locations = repository.GetLocations(subscriberId, page, pageSize);
            return View(locations);
        }

        [HttpGet]
        public ActionResult LocationAssigments(int locationId)
        {
            var assignments = repository.GetLocationAssigments(locationId);
            return View(assignments);
        }

        [HttpGet]
        public ActionResult LocationAssigmentsForProvider(int locationId, int providerId)
        {
            var assignments = repository.GetLocationAssigments(locationId);
            return View(assignments);
        }

        [HttpGet]
        public ActionResult Location(int id)
        {
            var location = repository.GetLocationById(id);
            return View(location);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Location location)
        {
            if (ModelState.IsValid)
            {
                repository.CreateLocation(location);
                return RedirectToAction("Location", "Home", new { id = location.Id });
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var location = repository.GetLocationById(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Location location)
        {
            repository.UpdateLocation(location);
            return RedirectToAction("Location", "Home", new { id = location.Id });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var location = repository.GetLocationById(id);
            repository.DeleteLocation(id);
            return RedirectToAction("Index", "Home");
        }
    }
}