using System.Collections.Generic;
using ParcelDelivery.BLL.DTO;

namespace ParcelDelivery.BLL.Services
{
    public interface IFeedbackService
    {
        /// <summary>
        /// Creates and shows feedbacks
        /// </summary>
        /// <param name="feedback"></param>
        void Create(FeedbackDTO feedback);
        IEnumerable<FeedbackDTO> GetProductFeedbacks(int carrierId);
        FeedbackDTO GetFeedback(int id);
        void DeleteFeedback(int id);
    }
}