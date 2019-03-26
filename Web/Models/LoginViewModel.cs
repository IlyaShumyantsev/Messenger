using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите Ваш логин")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите Ваш пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}