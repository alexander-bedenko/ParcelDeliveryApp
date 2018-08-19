using System.Collections.Generic;
using ParcelDelivery.BLL.DTO;

namespace ParcelDelivery.BLL.Services
{
    /// <summary>
    /// CRUD operations with property
    /// </summary>
    public interface IPropertyService
    {
        IEnumerable<PropertyDTO> ShowAllPropertiesById(int carrierId);
        IEnumerable<PropertyDTO> ShowAllProperties();
        void EditPropery(PropertyDTO property);
        void AddProperty(PropertyDTO property);
        PropertyDTO GetProperty(int id);
        void DeleteProperty(int id);
    }
}