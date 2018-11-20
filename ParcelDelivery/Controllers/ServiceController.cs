using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using ParcelDelivery.BLL.DTO;
using ParcelDelivery.BLL.Enums;
using ParcelDelivery.BLL.Services;
using ParcelDelivery.Models;
using ParcelDelivery.Services;

namespace ParcelDelivery.Controllers
{
    [Authorize]
    public class ServiceController : Controller
    {
        private readonly ICarrierService _carrierService;
        private readonly IServiceService _serviceService;

        public ServiceController(
            IServiceService propertyService, 
            ICarrierService carrierService)
        {
            _carrierService = carrierService;
            _serviceService = propertyService;
        }

        // GET: Property
        public ActionResult Index(int id)
        {
            var carrier = _carrierService.GetCarrier(id);
            var properties = Mapper.Map<IEnumerable<ServiceDTO>, IEnumerable<ServiceViewModel>>(_serviceService.ShowAllServicesById(carrier.Id));
            return View(properties);
        }

        [Authorize]
        public ActionResult Create()
        {
            return PartialView("_Create");
        }
        [HttpPost]
        public ActionResult Create(ServiceViewModel property)
        {
            var carrierId = Url.RequestContext.RouteData.Values["id"];

            if (ModelState.IsValid)
            {
                property.CarrierId = Convert.ToInt32(carrierId);
                _serviceService.AddService(Mapper.Map<ServiceDTO>(property));
            }

            return RedirectToAction("Index", "Service", new { id = carrierId });
        }

        public ActionResult Edit(int id)
        {
            var property = _serviceService.GetService(id);
            if (property != null)
            {
                return PartialView("_Edit", Mapper.Map<ServiceDTO, ServiceViewModel>(_serviceService.GetService(id)));
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServiceViewModel property)
        {
            var carrierId = _carrierService.GetCarrier(property.CarrierId).Id;
            property.CarrierId = Convert.ToInt32(carrierId);
            _serviceService.EditService(Mapper.Map<ServiceDTO>(property));
            return RedirectToAction("Index", "Service", new { id = carrierId });
        }

        public ActionResult Delete(int id)
        {
            var property = _serviceService.GetService(id);
            if (property != null)
            {
                return PartialView("_Delete", Mapper.Map<ServiceDTO, ServiceViewModel>(_serviceService.GetService(id)));
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteRecord(int id)
        {
            var property = _serviceService.GetService(id);

            if (property != null)
            {
                _serviceService.DeleteService(id);
            }

            return RedirectToAction("Index", "Service", new {id = property?.CarrierId });
        }

        [Authorize]
        public ActionResult FilterOptions()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult FilterOptions(ServiceViewModel property)
        {
            var listOfCarriers = _serviceService.FilteredList(Mapper.Map<ServiceViewModel, ServiceDTO>(property));

            TempData["carriers"] = Mapper.Map<IEnumerable<FilteredListDTO>, IEnumerable<FilteredListViewModel>>(listOfCarriers);

            return RedirectToAction("FilteredList", "Carrier");
        }
    }
}