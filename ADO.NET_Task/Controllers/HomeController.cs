﻿using ADO.NET_Task.Models;
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
        public ActionResult Index(int subscriberId = 1, int page = 1, int pageSize = 5)
        {
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;

            var locations = repository.GetLocations(subscriberId, page, pageSize);
            return View(locations);
        }

        [HttpGet]
        public ActionResult Location(int id)
        {
            var location = repository.GetLocationById(id);
            return View(location);
        }

        [HttpGet]
        public ActionResult LocationAssigments(int locationId, int? providerId = null)
        {
            IList<ProviderAssignment> assignments = new List<ProviderAssignment>();
            if (providerId != null)
            {
                ViewBag.providerId = providerId;
                assignments = repository.GetLocationAssigmentForProvider(locationId, providerId.Value);
                return View(assignments);
            }
            assignments = repository.GetLocationAssigments(locationId);
            return View(assignments);
        }

        [HttpGet]
        public ActionResult LocationAssigmentsForProvider(int locationId, int providerId)
        {
            var assignments = repository.GetLocationAssigments(locationId);
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
                repository.CreateLocation(location);
                return RedirectToAction("Index");
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
            return RedirectToAction("Index");
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