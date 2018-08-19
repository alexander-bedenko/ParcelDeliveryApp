using System.Collections.Generic;
using ParcelDelivery.BLL.DTO;

namespace ParcelDelivery.BLL.Services
{
    /// <summary>
    /// CRUD operations with carrier
    /// </summary>
    public interface ICarrierService
    {
        IEnumerable<CarrierDTO> ShowAllCarriers();
        IEnumerable<CarrierDTO> ShowAllCarriersById(int id);
        CarrierDTO GetCarrier(int id);
        CarrierDTO FindCarrier(string name);
        void EditCarrier(CarrierDTO carrierDto);
        void AddCarrier(CarrierDTO carrierDto);
        void DeleteCarrier(int id);
    }
}
