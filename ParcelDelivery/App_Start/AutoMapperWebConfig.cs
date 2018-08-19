using System;
using AutoMapper;
using ParcelDelivery.BLL.DTO;
using ParcelDelivery.Models;

namespace ParcelDelivery
{
    public class AutoMapperWebConfig
    {
        public static readonly Action<IMapperConfigurationExpression> ConfigAction = cfg =>
        {
            cfg.CreateMap<UserDTO, UserViewModel>().ReverseMap();
            cfg.CreateMap<CarrierDTO, CarrierViewModel>().ReverseMap();
            cfg.CreateMap<PropertyDTO, PropertyViewModel>().ReverseMap();
        };
    }
}