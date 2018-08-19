using System.ComponentModel.DataAnnotations;

namespace ParcelDelivery.Enums
{
    public enum TransportationArea
    {
        [Display(Name = "Город")]
        City,
        [Display(Name = "Область")]
        Region,
        [Display(Name = "Страна")]
        Country,
        [Display(Name = "Международный")]
        International
    }
}
