using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using ParcelDelivery.BLL.Infrastructure;
using ParcelDelivery.BLL.Modules;
using Serilog;
using Serilog.Events;

namespace ParcelDelivery
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AreaRegistration.RegisterAllAreas();

            Mapper.Initialize(x =>
            {
                AutoMapperWebConfig.ConfigAction.Invoke(x);
                AutoMapperBLLConfig.ConfigAction.Invoke(x);
            });

            NinjectModule serviceModule = new ServiceModule();
            var kernel = new StandardKernel(serviceModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            ModelValidatorProviders.Providers.Clear();
        }

        public void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();
            MvcApplication mvcApplication = sender as MvcApplication;
            HttpRequest request = null;
            if (mvcApplication != null) request = mvcApplication.Request;
        }

        private static void ConfigureLogger()
        {
            var date = DateTime.Now;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.RollingFile($@"D:\Log-{date:yyy-MM-dd}.txt", LogEventLevel.Verbose, "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}
