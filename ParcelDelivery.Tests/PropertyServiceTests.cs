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
        private PropertyDTO _property;
        private IPropertyService _propertyService;

        [SetUp]
        public void SetUp()
        {
            _propertyService = A.Fake<IPropertyService>();

            _property = new PropertyDTO
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
            A.CallTo(() => _propertyService.GetProperty(A<int>._)).Returns(_property);
            var property = _propertyService.GetProperty(1);
            
            Assert.NotNull(property);
            Assert.AreEqual(3.5m, _property.Coast);
        }

        [Test]
        public void WhenShowAllPropertiesByCarrierId()
        {
            var properties = new List<PropertyDTO>
            {
                new PropertyDTO()
                {
                    CarrierId = 1,
                    Coast = 3.5m,
                    MaxWeight = 3,
                    TransportationArea = TransportationArea.City,
                    TypeOfCargo = TypeOfCargo.Medium
                }
            };

            A.CallTo(() => _propertyService.ShowAllPropertiesById(A<int>._)).Returns(properties);
            var props = _propertyService.ShowAllPropertiesById(1);

            Assert.NotNull(props);
        }
    }
}
