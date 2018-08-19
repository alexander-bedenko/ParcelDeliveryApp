using System.ComponentModel.DataAnnotations;

namespace ParcelDelivery.Enums
{
    public enum TypeOfCargo
    {
        [Display(Name = "Малогабаритный")]
        Small,
        [Display(Name = "Средний")]
        Medium,
        [Display(Name = "Крупногабаритный")]
        Big
    } 
}
