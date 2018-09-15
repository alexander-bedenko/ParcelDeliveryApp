using System;
using System.Collections.Generic;
using AutoMapper;
using ParcelDelivery.BLL.DTO;
using ParcelDelivery.DAL.Entities;
using ParcelDelivery.DAL.UoW;
using Serilog;

namespace ParcelDelivery.BLL.Services.Impl
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeedbackService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(FeedbackDTO feedback)
        {
            try
            {
                var addFeedback = Mapper.Map<Feedback>(feedback);
                _unitOfWork.Repository<Feedback>().Create(addFeedback);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Log.Error(e, "Ошибка возникла при добавлениее нового отзыва.", e);
            }
        }

        public IEnumerable<FeedbackDTO> GetProductFeedbacks(int carrierId)
        {
            var feedbacks = _unitOfWork.Repository<Feedback>().Find(i => i.CarrierId == carrierId);
            return Mapper.Map<IEnumerable<Feedback>, List<FeedbackDTO>>(feedbacks);
        }

        public FeedbackDTO GetFeedback(int id)
        {
            var feedBack = _unitOfWork.Repository<Feedback>().Get(id);
            return Mapper.Map<Feedback, FeedbackDTO>(feedBack);
        }

        public void DeleteFeedback(int id)
        {
            try
            {
                var feedback = _unitOfWork.Repository<Feedback>().Get(id);

                _unitOfWork.Repository<Feedback>().Delete(feedback.Id);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Log.Error(e, "Ошибка возникла при удалении отзыва.", e);
            }
        }
    }
}
