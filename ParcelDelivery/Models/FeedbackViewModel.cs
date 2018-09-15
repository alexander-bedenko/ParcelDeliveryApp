using System;
using System.ComponentModel.DataAnnotations;

namespace ParcelDelivery.Models
{
    public class FeedbackViewModel
    {
        public int Id { get; set; }
        public int CarrierId { get; set; }
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Введите ваше имя")]
        public string Name { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Введите email")]
        public string Email { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Оценка")]
        [Required(ErrorMessage = "Оцените от 1 до 5")]
        public int Rating { get; set; }
        [Display(Name = "Отзыв")]
        [Required(ErrorMessage = "Оставте ваш отзыв")]
        public string Message { get; set; }
    }
}