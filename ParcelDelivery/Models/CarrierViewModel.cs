using System.ComponentModel.DataAnnotations;

namespace ParcelDelivery.Models
{
    public class CarrierViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Введите название компании")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите адрес компании")]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Введите телефон компании")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Введите краткое описание")]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}