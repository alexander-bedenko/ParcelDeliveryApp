using System.Data.Entity;
using ParcelDelivery.DAL.Entities;

namespace ParcelDelivery.DAL.EFDbContext
{
    public interface IParcelDeliveryContext
    {
        /// <summary>
        /// Creates context
        /// </summary>
        DbSet<User> Users { get; }
        DbSet<Carrier> Carriers { get; }
        DbSet<Property> Properties { get; }
        DbSet<Feedback> Feedbacks { get; }
        DbSet<T> Set<T>() where T : class;
    }
}