using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ParcelDelivery.Enums;

namespace ParcelDelivery.Models
{
    public class PropertyViewModel
    {
        public int Id { get; set; }
        public int CarrierId { get; set; }

        [Required(ErrorMessage = "Введите максимальный вес")]
        [Display(Name = "Максимальный вес")]
        public float MaxWeight { get; set; }

        [Required(ErrorMessage = "Введите стоимость перевозки")]
        [Display(Name = "Стоимость за 1 км.")]
        public decimal Coast { get; set; }

        [Required(ErrorMessage = "Выберите тип груза")]
        public TypeOfCargo TypeOfCargo { get; set; }

        [Required(ErrorMessage = "Выберите регион доставки")]
        public TransportationArea TransportationArea { get; set; }
    }
}