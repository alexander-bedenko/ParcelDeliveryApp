using System;

namespace ParcelDelivery.DAL.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public int? CarrierId { get; set; }
        public Carrier Carrier { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public string Message { get; set; }
    }
}
