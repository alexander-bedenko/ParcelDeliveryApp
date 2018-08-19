using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ParcelDelivery.DAL.EFDbContext.Impl;

namespace ParcelDelivery.DAL.Repositories.Impl
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ParcelDeliveryContext _context;

        public GenericRepository(ParcelDeliveryContext context)
        {
            _context = context;
        }

        public DbSet<T> Entities => _context.Set<T>();

        public virtual IEnumerable<T> GetAll()
        {
            return Entities;
        }

        public T Get(int id)
        {
            return Entities.Find(id);
        }

        public void Create(T item)
        {
            Entities.Add(item);
        }

        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return Entities.Where(predicate).ToList();
        }

        public void Delete(int? id)
        {
            T item = Entities.Find(id);
            if (item != null)
                Entities.Remove(item);
        }
    }
}
