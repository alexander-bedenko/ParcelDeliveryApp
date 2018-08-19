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
    public class PropertyService : IPropertyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PropertyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddProperty(PropertyDTO property)
        {
            try
            {
                var addProperty = Mapper.Map<Property>(property);
                _unitOfWork.Repository<Property>().Create(addProperty);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Log.Error(e, "Ошибка возникла при добавлениее новой опции.", e);
            }
        }

        public PropertyDTO GetProperty(int id)
        {
            var property = _unitOfWork.Repository<Property>().Get(id);
            return Mapper.Map<Property, PropertyDTO>(property);
        }

        public void EditPropery(PropertyDTO propertyDto)
        {
            try
            {
                var property = _unitOfWork.Repository<Property>().Get(propertyDto.Id);

                property.Coast = propertyDto.Coast;
                property.MaxWeight = propertyDto.MaxWeight;
                property.TypeOfCargo = (TypeOfCargo)propertyDto.TypeOfCargo;
                property.TransportationArea = (TransportationArea)propertyDto.TransportationArea;

                _unitOfWork.Repository<Property>().Update(property);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Log.Error(e, "Ошибка возникла при изменении услуги.", e);
            }
        }

        public void DeleteProperty(int id)
        {
            try
            {
                var property = _unitOfWork.Repository<Property>().Get(id);

                _unitOfWork.Repository<Property>().Delete(property.Id);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Log.Error(e, "Ошибка возникла при удалении услуги.", e);
            }
        }

        public IEnumerable<PropertyDTO> ShowAllPropertiesById(int carrierId)
        {
            var properties = _unitOfWork.Repository<Property>().Find(i => i.CarrierId == carrierId);
            return Mapper.Map<IEnumerable<Property>, List<PropertyDTO>>(properties);
        }

        public IEnumerable<PropertyDTO> ShowAllProperties()
        {
            var properties = _unitOfWork.Repository<Property>().GetAll();
            return Mapper.Map<IEnumerable<Property>, List<PropertyDTO>>(properties);
        }
    }
}
