using System.ComponentModel.DataAnnotations;

namespace cihaztakip.webui.Models
{

    //Class for editing device with user data 
    public class UserDeviceEditModel
    {
        public int UserDeviceId { get; set; }
        public int DeviceId { get; set; }
        [Required()]
        public string DeviceName { get; set; }
        public string UserId { get; set; }

        [Required()]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "hata EmailAddress")]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }

    }
}
