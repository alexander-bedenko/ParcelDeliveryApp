using ParcelDelivery.DAL.Repositories;

namespace ParcelDelivery.DAL.UoW
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Creates T entities
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <returns>Returns entity</returns>
        IGenericRepository<T> Repository<T>() where T : class;
        void Commit();
    }
}