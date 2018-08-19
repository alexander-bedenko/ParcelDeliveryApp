using System.Data.Entity;
using ParcelDelivery.DAL.Entities;

namespace ParcelDelivery.DAL.EFDbContext.Impl
{
    public class ParcelDeliveryContextInitializer : CreateDatabaseIfNotExists<ParcelDeliveryContext>
    {
        protected override void Seed(ParcelDeliveryContext db)
        {
            User user = new User() { Login = "Admin", Password = "嚻鱉鹑ﻷ걐ﰠⱹ姭", Email = "zylon@mail.ru" };

            db.Users.Add(user);
            db.SaveChanges();
        }
    }
}
