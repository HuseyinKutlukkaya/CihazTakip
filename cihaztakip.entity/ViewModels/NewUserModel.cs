using System.ComponentModel.DataAnnotations;

namespace cihaztakip.entity.ViewModels
{
    public class NewUserModel
    {
        [Display(Name = "İsim")]
        [Required(ErrorMessage = "İsim alanı gereklidir.")]
        public string FirstName { get; set; }

        [Display(Name = "Soyisim")]
        [Required(ErrorMessage = "Soyisim alanı gereklidir.")]
        public string LastName { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı Adı alanı gereklidir.")]
        public string UserName { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre alanı gereklidir.")]
        [DataType(DataType.Password, ErrorMessage = "Şifre geçersiz.")]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrarı")]
        [Required(ErrorMessage = "Şifre Tekrarı alanı gereklidir.")]
        [DataType(DataType.Password, ErrorMessage = "Şifreler uyuşmuyor.")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string RePassword { get; set; }

        [Display(Name = "E-Posta")]
        [Required(ErrorMessage = "E-Posta alanı gereklidir.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Geçersiz bir e-posta adresi.")]
        public string Email { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "Rol alanı gereklidir.")]
        public string Role { get; set; }

        
    }
}
