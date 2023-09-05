using System.ComponentModel.DataAnnotations;

namespace cihaztakip.entity.ViewModels
{
    public class DeviceModel
    {
        public int DeviceId { get; set; }

        [Display(Name = "Cihaz Adı")]
        [Required(ErrorMessage = "Cihaz Adı Boş Olamaz")]
        public string Name { get; set; }
    }
}
