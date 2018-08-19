using System;
using System.Collections.Generic;

namespace ParcelDelivery.DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// CRUD operations
        /// </summary>
        /// <returns>Context</returns>
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int? id);
    }
}