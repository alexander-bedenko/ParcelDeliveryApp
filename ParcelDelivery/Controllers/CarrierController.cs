using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AutoMapper;
using ParcelDelivery.BLL.DTO;
using ParcelDelivery.BLL.Services;
using ParcelDelivery.Models;
using PagedList.Mvc;
using PagedList;
using ParcelDelivery.BLL.Enums;

namespace ParcelDelivery.Controllers
{
    public class CarrierController : Controller
    {
        private readonly ICarrierService _carrierService;
        private readonly IUserService _userService;
        private readonly IFeedbackService _feedbackService;

        public CarrierController(
            ICarrierService carrierService,
            IUserService userService,
            IFeedbackService feedbackService)
        {
            _carrierService = carrierService;
            _userService = userService;
            _feedbackService = feedbackService;
        }

        // GET: Carrier
        public ActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            double averageRate = 0;
            int sum = 0;
            
            var carrier = Mapper.Map<IEnumerable<CarrierDTO>, IEnumerable<CarrierViewModel>>(_carrierService.ShowAllCarriers());

            foreach (var item in carrier)
            {
                var rate = _feedbackService.GetProductFeedbacks(item.Id);
                foreach (var k in rate)
                {
                    sum += k.Rating;
                }
                if(rate.Count() != 0)
                    averageRate = sum / rate.Count();
                ViewData.Add($"{item.Id}",averageRate);
                averageRate = 0;
                sum = 0;
            }
            
            return View(carrier.ToPagedList(pageNumber, pageSize));
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            var carrier = _carrierService.GetCarrier(id);
            if (carrier != null)
            {
                var carrierVM = Mapper.Map<CarrierDTO, CarrierViewModel>(_carrierService.GetCarrier(id));
                return PartialView("_Details", carrierVM);
            }
            return View("Index");
        }

        [Authorize]
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarrierViewModel carrier)
        {
            var userName = User.Identity.Name;
            var userId = _userService.FindUser(userName).Id;

            if (ModelState.IsValid)
            {
                var registeredCarrier = _carrierService.FindCarrier(Mapper.Map<CarrierDTO>(carrier).Name);
                if (registeredCarrier != null)
                {
                    ModelState.AddModelError("", "Такой курьер уже существует.");
                }
                else
                {
                    carrier.UserId = userId;
                    _carrierService.AddCarrier(Mapper.Map<CarrierDTO>(carrier));
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var carrier = _carrierService.GetCarrier(id);
            if (carrier != null)
            {
                return PartialView("_Edit", Mapper.Map<CarrierDTO, CarrierViewModel>(_carrierService.GetCarrier(id)));
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CarrierViewModel carrier)
        {
            _carrierService.EditCarrier(Mapper.Map<CarrierDTO>(carrier));
            
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var carrier = _carrierService.GetCarrier(id);
            if (carrier != null)
            {
                return PartialView("_Delete", Mapper.Map<CarrierDTO, CarrierViewModel>(_carrierService.GetCarrier(id)));
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteRecord(int id)
        {
            var carrier = _carrierService.GetCarrier(id);

            if (carrier != null)
            {
                _carrierService.DeleteCarrier(id);
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult FilteredList()
        {
            double averageRate = 0;
            int sum = 0;

            var listOfCarriers = (IEnumerable<FilteredListViewModel>)TempData["carriers"];

            foreach (var item in listOfCarriers)
            {
                var rate = _feedbackService.GetProductFeedbacks(item.CarrierId);
                foreach (var k in rate)
                {
                    sum += k.Rating;
                }
                if (rate.Count() != 0)
                    averageRate = sum / rate.Count();
                ViewData.Add($"{item.CarrierId}", averageRate);
                averageRate = 0;
                sum = 0;
            }

            return View(listOfCarriers);
        }
    }
}