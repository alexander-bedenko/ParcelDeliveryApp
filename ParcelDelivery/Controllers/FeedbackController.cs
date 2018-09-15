using AutoMapper;
using ParcelDelivery.BLL.DTO;
using ParcelDelivery.BLL.Services;
using ParcelDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ParcelDelivery.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly ICarrierService _carrierService;
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(
            ICarrierService carrierService, 
            IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
            _carrierService = carrierService;
        }

        public ActionResult CarrierFeedbacks(int id)
        {
            var carrier = _carrierService.GetCarrier(id);
            var feedBacks = Mapper.Map<IEnumerable<FeedbackDTO>, IEnumerable<FeedbackViewModel>>(_feedbackService.GetProductFeedbacks(carrier.Id));
            return View(feedBacks);
        }

        public ActionResult Create()
        {
            var carrierId = Url.RequestContext.RouteData.Values["id"];

            var feedback = _feedbackService.GetProductFeedbacks(Convert.ToInt32(carrierId)).FirstOrDefault();
            return PartialView("_Create", Mapper.Map<FeedbackDTO, FeedbackViewModel>(feedback));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FeedbackViewModel feedback)
        {
            if (ModelState.IsValid)
            {
                var carrierId = Url.RequestContext.RouteData.Values["id"];
                feedback.CarrierId = Convert.ToInt32(carrierId);
                feedback.Date = DateTime.Now;
                _feedbackService.Create(Mapper.Map<FeedbackDTO>(feedback));
            }
            return RedirectToAction("CarrierFeedbacks", "Feedback", feedback);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var feedBack = _feedbackService.GetFeedback(id);
            if (feedBack != null)
            {
                return PartialView("_Delete", Mapper.Map<FeedbackDTO, FeedbackViewModel>(_feedbackService.GetFeedback(id)));
            }
            return View("CarrierFeedbacks");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteRecord(int id)
        {
            var feedBack = _feedbackService.GetFeedback(id);

            if (feedBack != null)
            {
                _feedbackService.DeleteFeedback(id);
            }

            return RedirectToAction("CarrierFeedbacks", "Feedback", new { id = feedBack?.CarrierId });
        }
    }
}