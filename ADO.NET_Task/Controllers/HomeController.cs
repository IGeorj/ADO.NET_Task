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
        private readonly ILocationRepository _locationRepository;
        
        public HomeController (ILocationRepository locationRepository)
	    {
            this._locationRepository = locationRepository;
	    }
        
        [HttpGet]
        public ActionResult Index(int subscriberId = 1, int page = 1, int pageSize = 5)
        {
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;

            var locations = _locationRepository.GetLocations(subscriberId, page, pageSize);
            return View(locations);
        }

        [HttpGet]
        public ActionResult Location(int id)
        {
            var location = _locationRepository.GetLocationById(id);
            return View(location);
        }

        [HttpGet]
        public ActionResult LocationAssigments(int locationId, int? providerId = null)
        {
            IList<ProviderAssignment> assignments = new List<ProviderAssignment>();
            if (providerId != null)
            {
                ViewBag.providerId = providerId;
                assignments = _locationRepository.GetLocationAssigmentForProvider(locationId, providerId.Value);
                return View(assignments);
            }
            assignments = _locationRepository.GetLocationAssigments(locationId);
            return View(assignments);
        }

        [HttpGet]
        public ActionResult LocationAssigmentsForProvider(int locationId, int providerId)
        {
            var assignments = _locationRepository.GetLocationAssigments(locationId);
            return View(assignments);
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
                var loc = _locationRepository.CreateLocation(location);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var location = _locationRepository.GetLocationById(id);
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
            _locationRepository.UpdateLocation(location);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var location = _locationRepository.GetLocationById(id);
            _locationRepository.DeleteLocation(id);
            return RedirectToAction("Index", "Home");
        }
    }
}