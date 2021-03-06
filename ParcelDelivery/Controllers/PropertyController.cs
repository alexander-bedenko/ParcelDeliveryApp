﻿using System;
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
    public class PropertyController : Controller
    {
        private readonly ICarrierService _carrierService;
        private readonly IPropertyService _propertyService;

        public PropertyController(
            IPropertyService propertyService, 
            ICarrierService carrierService)
        {
            _carrierService = carrierService;
            _propertyService = propertyService;
        }

        // GET: Property
        public ActionResult Index(int id)
        {
            var carrier = _carrierService.GetCarrier(id);
            var properties = Mapper.Map<IEnumerable<PropertyDTO>, IEnumerable<PropertyViewModel>>(_propertyService.ShowAllPropertiesById(carrier.Id));
            return View(properties);
        }

        [Authorize]
        public ActionResult Create()
        {
            return PartialView("_Create");
        }
        [HttpPost]
        public ActionResult Create(PropertyViewModel property)
        {
            var carrierId = Url.RequestContext.RouteData.Values["id"];

            if (ModelState.IsValid)
            {
                property.CarrierId = Convert.ToInt32(carrierId);
                _propertyService.AddProperty(Mapper.Map<PropertyDTO>(property));
            }

            return RedirectToAction("Index", "Property", new { id = carrierId });
        }

        public ActionResult Edit(int id)
        {
            var property = _propertyService.GetProperty(id);
            if (property != null)
            {
                return PartialView("_Edit", Mapper.Map<PropertyDTO, PropertyViewModel>(_propertyService.GetProperty(id)));
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PropertyViewModel property)
        {
            var carrierId = _carrierService.GetCarrier(property.CarrierId).Id;
            property.CarrierId = Convert.ToInt32(carrierId);
            _propertyService.EditPropery(Mapper.Map<PropertyDTO>(property));
            return RedirectToAction("Index", "Property", new { id = carrierId });
        }

        public ActionResult Delete(int id)
        {
            var property = _propertyService.GetProperty(id);
            if (property != null)
            {
                return PartialView("_Delete", Mapper.Map<PropertyDTO, PropertyViewModel>(_propertyService.GetProperty(id)));
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteRecord(int id)
        {
            var property = _propertyService.GetProperty(id);

            if (property != null)
            {
                _propertyService.DeleteProperty(id);
            }

            return RedirectToAction("Index", "Property", new {id = property?.CarrierId });
        }

        [Authorize]
        public ActionResult FilterOptions()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult FilterOptions(PropertyViewModel property)
        {
            var listOfCarriers = new List<FilteredListViewModel>();

            var filteredList = _propertyService.ShowAllProperties()
                .Where(toc => toc.TypeOfCargo.Equals((TypeOfCargo)property.TypeOfCargo))
                .Where(ta => ta.TransportationArea.Equals((TransportationArea)property.TransportationArea))
                .Where(w => w.MaxWeight > property.MaxWeight)
                .Select(c => c.CarrierId).ToList();
            
            foreach (var key in filteredList)
            {
                listOfCarriers.Add(new FilteredListViewModel()
                {
                    CarrierId = _carrierService.GetCarrier(key).Id,
                    Name = _carrierService.GetCarrier(key).Name,
                    Address = _carrierService.GetCarrier(key).Address,
                    Phone = _carrierService.GetCarrier(key).Phone,
                    Description = _carrierService.GetCarrier(key).Description,
                    Coast = _propertyService.ShowAllPropertiesById(key).FirstOrDefault(p => p.CarrierId == key).Coast * (decimal)property.Distance,
                    Type = _propertyService.ShowAllPropertiesById(key).FirstOrDefault(p => p.CarrierId == key).TransportationArea.GetDisplayName(),
                    Time = TimeInTransit(
                        property.Distance, 
                        _propertyService.ShowAllPropertiesById(key).FirstOrDefault(p => p.CarrierId == key).TransportationArea.GetDisplayName())
                });
            }

            TempData["carriers"] = listOfCarriers;

            return RedirectToAction("FilteredList", "Carrier");
        }

        private double TimeInTransit(double distance, string type)
        {
            double time = 0;

            switch (type)
            {
                case "City":
                    time = distance / 17;
                    break;

                case "Region":
                    time = distance / 53;
                    break;

                case "Country":
                    time = distance / 70;
                    break;

                case "International":
                    time = distance / 90;
                    break;
            }

            return Math.Round(time, 2);
        }
    }
}