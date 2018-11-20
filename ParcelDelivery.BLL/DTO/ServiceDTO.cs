using ParcelDelivery.BLL.Enums;

namespace ParcelDelivery.BLL.DTO
{
    public class ServiceDTO
    {
        public int Id { get; set; }
        public int CarrierId { get; set; }
        public float MaxWeight { get; set; }
        public decimal Coast { get; set; }
        public TypeOfCargo TypeOfCargo { get; set; }
        public TransportationArea TransportationArea { get; set; }
        public double Distance { get; set; }
    }
}
