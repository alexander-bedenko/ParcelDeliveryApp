using System.ComponentModel.DataAnnotations;

namespace ParcelDelivery.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Специальные символы недопустимы")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Введите логин")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Специальные символы недопустимы")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Необходим пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Разные пароли.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Необходим адрес электронной почты")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Неверный формат адреса электронной почты")]
        public string Email { get; set; }
    }
}