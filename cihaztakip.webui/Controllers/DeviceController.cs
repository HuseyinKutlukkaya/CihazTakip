using cihaztakip.business.Abstract;
using cihaztakip.entity;
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

        public IActionResult CreateDevice()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateDevice(DeviceModel model)
        {
            if (ModelState.IsValid)
            {
                Device device = new Device() { Name = model.Name };
               _deviceService.Create(device);
                return RedirectToAction("DeviceList");
            }
            ModelState.AddModelError("", "Bir Sıkıntı var tekrar dene.");
            return View();
        }

    }
}
