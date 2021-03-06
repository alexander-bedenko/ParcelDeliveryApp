﻿
using System.Collections.Generic;

namespace ParcelDelivery.DAL.Entities
{
    public class Carrier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public ICollection<Service> Services { get; set; } = new HashSet<Service>();
        public virtual ICollection<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();
    }
}
