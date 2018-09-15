using System;

namespace ParcelDelivery.BLL.DTO
{
    public class FeedbackDTO
    {
        public int Id { get; set; }
        public int CarrierId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public string Message { get; set; }
    }
}
