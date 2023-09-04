using System.ComponentModel.DataAnnotations;

namespace cihaztakip.webui.Models
{
    public class RegisterModel
    {
        [Display(Name = "İsim")]
        [Required(ErrorMessage = "İsim Boş Olamaz")]
        public string FirstName { get; set; }

        [Display(Name = "Soyisim")]
        [Required(ErrorMessage = "Soyisim Boş Olamaz")]
        public string LastName { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı Adı Boş Olamaz")]
        public string UserName { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre Boş Olamaz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrarı")]
        [Required(ErrorMessage = "Şifre Tekrarı Boş Olamaz")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string RePassword { get; set; }

        [Display(Name = "E-Posta")]
        [Required(ErrorMessage = "E-Posta Boş Olamaz")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Geçersiz E-Posta Adresi")]
        public string Email { get; set; }




    }
}
