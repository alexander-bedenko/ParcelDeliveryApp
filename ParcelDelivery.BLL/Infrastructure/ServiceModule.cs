using Ninject.Modules;
using ParcelDelivery.BLL.Services;
using ParcelDelivery.BLL.Services.Impl;
using ParcelDelivery.DAL.UoW;
using ParcelDelivery.DAL.UoW.Impl;

namespace ParcelDelivery.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IUserService>().To<UserService>();
            Bind<ICarrierService>().To<CarrierService>();
            Bind<IPropertyService>().To<PropertyService>();
        }
    }
}
