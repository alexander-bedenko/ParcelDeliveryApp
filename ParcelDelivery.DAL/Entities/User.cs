using System.Collections.Generic;

namespace ParcelDelivery.DAL.Entities
{
    public class User
    {
        public User()
        {
            Carriers = new List<Carrier>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<Carrier> Carriers { get; set; }
    }
}
