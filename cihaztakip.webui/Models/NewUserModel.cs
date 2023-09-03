using System.ComponentModel.DataAnnotations;

namespace cihaztakip.webui.Models
{
    public class NewUserModel
    {
        [Required()]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "hata Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password,ErrorMessage = "hata Password")]
        [Compare("Password")]
        public string RePassword { get; set; }


        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "hata EmailAddress")]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
