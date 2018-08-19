using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ParcelDelivery.BLL.DTO;
using ParcelDelivery.DAL.Entities;
using ParcelDelivery.DAL.UoW;
using Serilog;

namespace ParcelDelivery.BLL.Services.Impl
{
    public class CarrierService : ICarrierService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarrierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CarrierDTO> ShowAllCarriers()
        {
            var carriers = _unitOfWork.Repository<Carrier>().GetAll();
            return Mapper.Map<IEnumerable<Carrier>, List<CarrierDTO>>(carriers);
        }

        public IEnumerable<CarrierDTO> ShowAllCarriersById(int id)
        {
            var carriers = _unitOfWork.Repository<Carrier>().Find(i => i.Id == id);
            return Mapper.Map<IEnumerable<Carrier>, List<CarrierDTO>>(carriers);
        }

        public CarrierDTO GetCarrier(int id)
        {
            var carrier = _unitOfWork.Repository<Carrier>().Get(id);
            return Mapper.Map<Carrier, CarrierDTO>(carrier);
        }

        public CarrierDTO FindCarrier(string name)
        {
            var carrier = _unitOfWork.Repository<Carrier>().Find(u => u.Name == name).FirstOrDefault();
            return Mapper.Map<Carrier, CarrierDTO>(carrier);
        }

        public void AddCarrier(CarrierDTO carrierDto)
        {
            try
            {
                var addCarrier = Mapper.Map<Carrier>(carrierDto);
                _unitOfWork.Repository<Carrier>().Create(addCarrier);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Log.Error(e, "Ошибка возникла при добавлениее нового курьера.", e);
            }
        }

        public void EditCarrier(CarrierDTO carrierDto)
        {
            try
            {
                var carrier = _unitOfWork.Repository<Carrier>().Get(carrierDto.Id);

                carrier.Name = carrierDto.Name;
                carrier.Address = carrierDto.Address;
                carrier.Phone = carrierDto.Phone;
                carrier.Description = carrierDto.Description;

                _unitOfWork.Repository<Carrier>().Update(carrier);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Log.Error(e, "Ошибка возникла при изменении курьера.", e);
            }
        }

        public void DeleteCarrier(int id)
        {
            try
            {
                var carrier = _unitOfWork.Repository<Carrier>().Find(u => u.Id == id).FirstOrDefault();

                _unitOfWork.Repository<Carrier>().Delete(carrier?.Id);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Log.Error(e, "Ошибка возникла при удалении курьера.", e);
            }
        }
    }
}