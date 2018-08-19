using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using ParcelDelivery.BLL.DTO;
using ParcelDelivery.BLL.Services;

namespace ParcelDelivery.Tests
{
    [TestFixture]
    public class CarrierServiceTests
    {
        private CarrierDTO _carrier;
        private ICarrierService _carrierService;

        [SetUp]
        public void SetUp()
        {
            _carrierService = A.Fake<ICarrierService>();

            _carrier = new CarrierDTO
            {
                Id = 1,
                Name = "Быстрая доставка",
                Address = "пр-т Дзержинского 104, оф. 18",
                Phone = "+375 29 6214177",
                Description = "Быстрая доставка мелких товаров по городу.",
            };
        }

        [Test]
        public void WhenGetCarrierById()
        {
            A.CallTo(() => _carrierService.GetCarrier(A<int>._)).Returns(_carrier);
            var carrierById = _carrierService.GetCarrier(1);
            
            Assert.AreEqual("Быстрая доставка", carrierById.Name);
            Assert.AreEqual("пр-т Дзержинского 104, оф. 18", carrierById.Address);
            Assert.AreEqual("+375 29 6214177", carrierById.Phone);
            Assert.AreEqual("Быстрая доставка мелких товаров по городу.", carrierById.Description);
        }

        [Test]
        public void WhenGetAllCarriers()
        {
            A.CallTo(() => _carrierService.ShowAllCarriers()).Returns(new List<CarrierDTO>(){_carrier});
            var carriers = _carrierService.ShowAllCarriers();

            Assert.IsNotEmpty(carriers);
        }

        [Test]
        public void WhenFindCarrierByName()
        {
            A.CallTo(() => _carrierService.FindCarrier(A<string>._)).Returns(_carrier);
            var carrierByName = _carrierService.FindCarrier("Быстрая доставка");

            Assert.AreEqual("Быстрая доставка", carrierByName?.Name);
            Assert.AreEqual("пр-т Дзержинского 104, оф. 18", carrierByName?.Address);
            Assert.AreEqual("+375 29 6214177", carrierByName?.Phone);
            Assert.AreEqual("Быстрая доставка мелких товаров по городу.", carrierByName?.Description);
        }
    }
}
