using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ParcelDelivery.BLL.DTO;
using ParcelDelivery.DAL.Entities;
using ParcelDelivery.DAL.Enums;
using ParcelDelivery.DAL.UoW;
using Serilog;

namespace ParcelDelivery.BLL.Services.Impl
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddService(ServiceDTO service)
        {
            try
            {
                var addService = Mapper.Map<Service>(service);
                _unitOfWork.Repository<Service>().Create(addService);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Log.Error(e, "Ошибка возникла при добавлениее новой опции.", e);
            }
        }

        public ServiceDTO GetService(int id)
        {
            var service = _unitOfWork.Repository<Service>().Get(id);
            return Mapper.Map<Service, ServiceDTO>(service);
        }

        public void EditService(ServiceDTO serviceDto)
        {
            try
            {
                var service = _unitOfWork.Repository<Service>().Get(serviceDto.Id);

                service.Coast = serviceDto.Coast;
                service.MaxWeight = serviceDto.MaxWeight;
                service.TypeOfCargo = (TypeOfCargo)serviceDto.TypeOfCargo;
                service.TransportationArea = (TransportationArea)serviceDto.TransportationArea;

                _unitOfWork.Repository<Service>().Update(service);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Log.Error(e, "Ошибка возникла при изменении услуги.", e);
            }
        }

        public void DeleteService(int id)
        {
            try
            {
                var service = _unitOfWork.Repository<Service>().Get(id);

                _unitOfWork.Repository<Service>().Delete(service.Id);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Log.Error(e, "Ошибка возникла при удалении услуги.", e);
            }
        }

        public IEnumerable<ServiceDTO> ShowAllServicesById(int carrierId)
        {
            var services = _unitOfWork.Repository<Service>().Find(i => i.CarrierId == carrierId);
            return Mapper.Map<IEnumerable<Service>, List<ServiceDTO>>(services);
        }

        public IEnumerable<ServiceDTO> ShowAllServices()
        {
            var services = _unitOfWork.Repository<Service>().GetAll();
            return Mapper.Map<IEnumerable<Service>, List<ServiceDTO>>(services);
        }

        public IEnumerable<FilteredListDTO> FilteredList(ServiceDTO serviceDto)
        {
            var listOfCarriers = new List<FilteredListDTO>();

            var filteredList = _unitOfWork.Repository<Service>().GetAll()
                .Where(toc => toc.TypeOfCargo.Equals((TypeOfCargo)serviceDto.TypeOfCargo))
                .Where(ta => ta.TransportationArea.Equals((TransportationArea)serviceDto.TransportationArea))
                .Where(w => w.MaxWeight > serviceDto.MaxWeight)
                .Select(c => c.CarrierId).ToList();

            foreach (var key in filteredList)
            {
                listOfCarriers.Add(new FilteredListDTO()
                {
                    CarrierId = _unitOfWork.Repository<Carrier>().Get(key).Id,
                    Name = _unitOfWork.Repository<Carrier>().Get(key).Name,
                    Address = _unitOfWork.Repository<Carrier>().Get(key).Address,
                    Phone = _unitOfWork.Repository<Carrier>().Get(key).Phone,
                    Description = _unitOfWork.Repository<Carrier>().Get(key).Description,
                    Coast = _unitOfWork.Repository<Service>().GetAll().FirstOrDefault(p => p.CarrierId == key).Coast * (decimal)serviceDto.Distance,
                    Type = _unitOfWork.Repository<Service>().GetAll().FirstOrDefault(p => p.CarrierId == key).TransportationArea.GetDisplayName(),
                    Time = TimeInTransit(
                        serviceDto.Distance,
                        _unitOfWork.Repository<Service>().GetAll().FirstOrDefault(p => p.CarrierId == key).TransportationArea.GetDisplayName())
                });
            }

            return listOfCarriers;
        }

        private double TimeInTransit(double distance, string type)
        {
            double time = 0;

            switch (type)
            {
                case "City":
                    time = distance / 17;
                    break;

                case "Region":
                    time = distance / 53;
                    break;

                case "Country":
                    time = distance / 70;
                    break;

                case "International":
                    time = distance / 90;
                    break;
            }

            return Math.Round(time, 2);
        }
    }
}
