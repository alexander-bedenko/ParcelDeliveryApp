using System;
using AutoMapper;
using ParcelDelivery.BLL.DTO;
using ParcelDelivery.DAL.Entities;

namespace ParcelDelivery.BLL.Modules
{
    public class AutoMapperBLLConfig
    {
        public static readonly Action<IMapperConfigurationExpression> ConfigAction = cfg =>
        {
            cfg.CreateMap<User, UserDTO>().ReverseMap();
            cfg.CreateMap<Carrier, CarrierDTO>().ReverseMap();
            cfg.CreateMap<Service, ServiceDTO>().ReverseMap();
            cfg.CreateMap<Feedback, FeedbackDTO>().ReverseMap();
        };
    }
}
