using System.Collections.Generic;
using ParcelDelivery.BLL.DTO;

namespace ParcelDelivery.BLL.Services
{
    /// <summary>
    /// CRUD operations with property
    /// </summary>
    public interface IServiceService
    {
        IEnumerable<ServiceDTO> ShowAllServicesById(int carrierId);
        IEnumerable<ServiceDTO> ShowAllServices();
        IEnumerable<FilteredListDTO> FilteredList(ServiceDTO serviceDto);
        void EditService(ServiceDTO service);
        void AddService(ServiceDTO service);
        ServiceDTO GetService(int id);
        void DeleteService(int id);
    }
}