namespace cihaztakip.entity.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    namespace cihaztakip.entity.ViewModels
    {
        public class UserDetailsModel
        {

            public string UserId { get; set; }

            [Display(Name = "Kullanıcı Adı")]
            [Required(ErrorMessage = "Kullanıcı Adı alanı gereklidir.")]
            public string UserName { get; set; }

            [Display(Name = "İsim")]
            [Required(ErrorMessage = "İsim alanı gereklidir.")]
            public string FirstName { get; set; }

            [Display(Name = "Soyisim")]
            [Required(ErrorMessage = "Soyisim alanı gereklidir.")]
            public string LastName { get; set; }

            [Display(Name = "E-Posta")]
            [DataType(DataType.EmailAddress, ErrorMessage = "Geçersiz bir e-posta adresi.")]
            [Required(ErrorMessage = "E-Posta alanı gereklidir.")]
            public string Email { get; set; }

            [Display(Name = "Rol")]
            [Required(ErrorMessage = "Rol alanı gereklidir.")]
            public string Role { get; set; }
        }
    }

}
