using System.ComponentModel.DataAnnotations;

namespace Reports.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
