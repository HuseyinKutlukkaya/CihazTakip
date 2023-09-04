using System.ComponentModel.DataAnnotations;

namespace cihaztakip.entity.ViewModels
{
    public class LoginModel
    {
        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı Adı Boş Olamaz")]
        public string UserName { get; set; }

        
        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre Boş Olamaz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
