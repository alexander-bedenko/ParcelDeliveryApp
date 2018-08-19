using System;
using System.Collections.Generic;
using System.Linq;
using ParcelDelivery.DAL.EFDbContext.Impl;
using ParcelDelivery.DAL.Repositories;
using ParcelDelivery.DAL.Repositories.Impl;

namespace ParcelDelivery.DAL.UoW.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ParcelDeliveryContext _entities;
        public Dictionary<Type, object> Repositories = new Dictionary<Type, object>();

        public UnitOfWork()
        {
            _entities = new ParcelDeliveryContext();
        }
        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (Repositories.Keys.Contains(typeof(T)))
            {
                return Repositories[typeof(T)] as IGenericRepository<T>;
            }

            IGenericRepository<T> repo = new GenericRepository<T>(_entities);
            Repositories.Add(typeof(T), repo);
            return repo;
        }

        public void Commit()
        {
            _entities.SaveChanges();
        }
    }
}
