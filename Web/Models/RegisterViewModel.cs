using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите Ваш логин")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Повторите ввод пароля")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Введите Ваш email")]
        [RegularExpression(@"^[a-zA-Z0-9.-]{1,20}@[a-zA-Z0-9]{1,20}\.[A-Za-z]{2,4}", ErrorMessage = "Неверный формат адреса")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Повторите ввод email")]
        [RegularExpression(@"^[a-zA-Z0-9.-]{1,20}@[a-zA-Z0-9]{1,20}\.[A-Za-z]{2,4}", ErrorMessage = "Неверный формат адреса")]
        [Compare("Email", ErrorMessage = "Адреса не совпадают")]
        public string ConfirmEmail { get; set; }

        [Required(ErrorMessage = "Введите Ваше имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите Вашу фамилию")]
        public string Surname { get; set; }
    }
}