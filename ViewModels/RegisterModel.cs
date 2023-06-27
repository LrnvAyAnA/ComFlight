using System.ComponentModel.DataAnnotations;

namespace ComFlight.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указано имя")]
        [MinLength(2,ErrorMessage ="Имя должно содержать больше 2 символов")]
        [MaxLength(20,ErrorMessage ="Имя должно содержать меньше 20 символов")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Не указан Email")]
        public string? Login { get; set; }

        [MinLength(8,ErrorMessage ="Пароль слишком короткий")]
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string? ConfirmPassword { get; set; }
    }
}
