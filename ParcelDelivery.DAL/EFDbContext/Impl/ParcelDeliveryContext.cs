using System.Data.Entity;
using ParcelDelivery.DAL.Entities;

namespace ParcelDelivery.DAL.EFDbContext.Impl
{
    public class ParcelDeliveryContext : DbContext, IParcelDeliveryContext
    {
        public ParcelDeliveryContext() :
            base("ParcelDeliveryDb")
        {
            Database.SetInitializer(new ParcelDeliveryContextInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Property> Properties { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(l => l.Carriers)
                .WithRequired(u => u.User)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .Property(l => l.Login)
                .HasMaxLength(20);

            modelBuilder.Entity<Carrier>()
                .HasMany(p => p.Properties)
                .WithRequired(c => c.Carrier)
                .HasForeignKey(c => c.CarrierId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
