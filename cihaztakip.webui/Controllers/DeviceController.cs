using cihaztakip.business.Abstract;
using cihaztakip.webui.Models;
using Microsoft.AspNetCore.Mvc;

namespace cihaztakip.webui.Controllers
{
    public class DeviceController : Controller
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;

        }
        public IActionResult DeviceList()
        {
            DeviceListViewModel list = new DeviceListViewModel();
            list.Devices=_deviceService.GetAllWithUserData();
            return View(list);
        }
    }
}
