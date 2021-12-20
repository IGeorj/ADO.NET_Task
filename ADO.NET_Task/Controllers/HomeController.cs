using ADO.NET_Task.Models;
using ADO.NET_Task.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ADO.NET_Task.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILocationRepository _locationRepository;
        
        public HomeController (ILocationRepository locationRepository)
	    {
            _locationRepository = locationRepository;
	    }
        
        [HttpGet]
        public async Task<ActionResult> Index(int subscriberId = 1,
                                              int page = 1,
                                              int pageSize = 5,
                                              CancellationToken token = default)
        {
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;

            var locations = _locationRepository.GetLocations(subscriberId, page, pageSize);
            return View(locations);
        }

        [HttpGet]
        public async Task<ActionResult> Location(int id, CancellationToken token)
        {
            var location = await _locationRepository.GetLocationByIdAsync(id, token);
            return View(location);
        }

        [HttpGet]
        public async Task<ActionResult> LocationAssigments(int locationId,
                                                           int? providerId = null,
                                                           CancellationToken token = default)
        {
            IList<ProviderAssignment> assignments = new List<ProviderAssignment>();
            if (providerId != null)
            {
                ViewBag.providerId = providerId;
                assignments = await _locationRepository.GetLocationAssigmentForProviderAsync(locationId,
                                                                                  providerId.Value,
                                                                                  token);
                return View(assignments);
            }
            assignments = await _locationRepository.GetLocationAssigmentsAsync(locationId, token);
            return View(assignments);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return await Task.Run(() => View());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Location location, CancellationToken token)
        {
            if (ModelState.IsValid)
            {
                var loc = await _locationRepository.CreateLocationAsync(location, token);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id, CancellationToken token)
        {
            var location = await _locationRepository.GetLocationByIdAsync(id, token);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Location location, CancellationToken token)
        {
            await _locationRepository.UpdateLocationAsync(location, token);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id, CancellationToken token)
        {
            await _locationRepository.GetLocationByIdAsync(id, token);
            _locationRepository.DeleteLocation(id);
            return RedirectToAction("Index");
        }
    }
}