using cihaztakip.entity;

namespace cihaztakip.entity.ViewModels
{
    public class DeviceListViewModel
    {
        public List<Device> Devices { get; set; }
        public DeviceListViewModel()
        {
            Devices = new List<Device>();
        }
    }
}
