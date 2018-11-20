using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using ParcelDelivery.BLL.DTO;
using ParcelDelivery.BLL.Enums;
using ParcelDelivery.BLL.Services;

namespace ParcelDelivery.Tests
{
    [TestFixture]
    public class PropertyServiceTests
    {
        private ServiceDTO _property;
        private IServiceService _propertyService;

        [SetUp]
        public void SetUp()
        {
            _propertyService = A.Fake<IServiceService>();

            _property = new ServiceDTO
            {
                CarrierId = 1,
                Coast = 3.5m,
                MaxWeight = 3,
                TransportationArea = TransportationArea.City,
                TypeOfCargo = TypeOfCargo.Medium
            };
        }

        [Test]
        public void WhenGetPropertyById()
        {
            A.CallTo(() => _propertyService.GetService(A<int>._)).Returns(_property);
            var property = _propertyService.GetService(1);
            
            Assert.NotNull(property);
            Assert.AreEqual(3.5m, _property.Coast);
        }

        [Test]
        public void WhenShowAllPropertiesByCarrierId()
        {
            var properties = new List<ServiceDTO>
            {
                new ServiceDTO()
                {
                    CarrierId = 1,
                    Coast = 3.5m,
                    MaxWeight = 3,
                    TransportationArea = TransportationArea.City,
                    TypeOfCargo = TypeOfCargo.Medium
                }
            };

            A.CallTo(() => _propertyService.ShowAllServicesById(A<int>._)).Returns(properties);
            var props = _propertyService.ShowAllServicesById(1);

            Assert.NotNull(props);
        }
    }
}
