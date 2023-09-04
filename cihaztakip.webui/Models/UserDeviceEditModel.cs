using System.ComponentModel.DataAnnotations;

namespace cihaztakip.webui.Models
{
    // Class for editing device with user data 
    public class UserDeviceEditModel
    {
        public int? UserDeviceId { get; set; }
        public int DeviceId { get; set; }

        [Required(ErrorMessage = "Cihaz Adı alanı boş bırakılamaz.")]
        public string DeviceName { get; set; }

        public string? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Geçersiz bir e-posta adresi.")]
        public string? Email { get; set; }
        public string?    Role { get; set; }
    }
}
