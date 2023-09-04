using System.ComponentModel.DataAnnotations;

namespace cihaztakip.entity.ViewModels
{
    // Class for editing device with user data 
    public class UserDeviceEditModel
    {
        public int? UserDeviceId { get; set; }
        public int DeviceId { get; set; }

        [Display(Name = "Cihaz Adı")]
        [Required(ErrorMessage = "Cihaz Adı alanı boş bırakılamaz.")]
        public string DeviceName { get; set; }

        public string? UserId { get; set; }

        [Display(Name = "Ad")]
        public string? FirstName { get; set; }

        [Display(Name = "Soyad")]
        public string? LastName { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public string? UserName { get; set; }

        [Display(Name = "E-Posta")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Geçersiz bir e-posta adresi.")]
        public string? Email { get; set; }

        [Display(Name = "Rol")]
        public string? Role { get; set; }
    }
}
