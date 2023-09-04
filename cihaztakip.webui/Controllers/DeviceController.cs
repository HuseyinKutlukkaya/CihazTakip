using cihaztakip.business.Abstract;
using cihaztakip.business.Concrete;
using cihaztakip.entity;
using cihaztakip.webui.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cihaztakip.webui.Controllers
{
    public class DeviceController : Controller
    {
        private readonly IDeviceService _deviceService;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IUserDeviceService _userDeviceService;
        public DeviceController(IDeviceService deviceService, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IUserDeviceService userDeviceService)
        {
            _deviceService = deviceService;
            _userManager = userManager;
            _roleManager = roleManager;
            _userDeviceService = userDeviceService;

        }
        public IActionResult DeviceList()
        {
            DeviceListViewModel list = new DeviceListViewModel();
            list.Devices = _deviceService.GetAllWithUserData();
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
                return Json(new { success = true, redirectUrl = Url.Action("DeviceList") });
            }
            ModelState.AddModelError("", "Bir Sıkıntı var tekrar dene.");
            return Json(new { success = false, message = "Cihaz oluşturulamadı." });
        }



        [HttpPost]
        public async Task<IActionResult> DeleteDevice(int deviceId)
        {
            var device =  _deviceService.GetById(deviceId);

            if (device != null)
            {
                _deviceService.Delete(device);
            }

           

            return RedirectToAction("DeviceList");
        }
        

        public async Task<IActionResult> EditDevice(int id)
        {

            UserDeviceEditModel model = new UserDeviceEditModel();
            var device = _deviceService.GetByIdWithUserDeviceData(id);

  
            model.DeviceId = device.DeviceId;
            model.DeviceName = device.Name;
            if (device.UserDevices.Count>0)
            {
                model.UserDeviceId = device.UserDevices[0].UserDeviceId;
                model.UserId = device.UserDevices[0].UserId;
                model.UserName = device.UserDevices[0].User.UserName;
                model.FirstName = device.UserDevices[0].User.FirstName;
                model.LastName = device.UserDevices[0].User.LastName;
                model.Email = device.UserDevices[0].User.Email;

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    model.Role = roles.FirstOrDefault();
                }

            }

            return View(model);
        }
        [HttpPost]
        public IActionResult EditDevice(UserDeviceEditModel model)
        {
            
         
                var device=_deviceService.GetById(model.DeviceId);
                device.Name = model.DeviceName;
                _deviceService.Update(device);
           
            return RedirectToAction("DeviceList");
        }


        [HttpPost]
        public IActionResult UpdateUser(string email, int deviceId,int userdeviceId)
        {
            var user = _userManager.FindByEmailAsync(email).Result;

            if (user != null)
            {

                // userdeviceId returns  -1 if device has  no user

                if (userdeviceId >0)// there is userdevice  so update the user id 
                {
                    UserDevice userDevice = _userDeviceService.GetById(userdeviceId);
                    UserDevice newuserdevice = new UserDevice() { UserId = user.Id, DeviceId = userDevice.DeviceId };
                    _userDeviceService.Update(newuserdevice,userDevice);
                }
                else// there no userdevice so create new one
                {
                    UserDevice userDevice = new UserDevice() { DeviceId=deviceId,UserId=user.Id };
             
                    _userDeviceService.Create(userDevice);
                }


                return Json(new { success = true, redirectUrl = Url.Action("EditDevice", new { id = deviceId }) });
            }
            else
            {
                // Kullanıcı bulunamadı, hata mesajı göster
                return Json(new { success = false, message = "Kullanıcı Bulunamadı!" });

            }
        }
        [HttpPost]
        public IActionResult DeleteUser(int deviceId, int userdeviceId)
        {
            var userdevice=_userDeviceService.GetById(userdeviceId);
            if (userdevice != null)
            {
                _userDeviceService.Delete(userdevice);

                return Json(new { success = true, redirectUrl = Url.Action("EditDevice", new { id = deviceId }) });
            }
            else
            {
                return Json(new { success = false, message = "Cihaza ait bir kullanıcı bulunmamaktadır!" });
            }
        }

    }
}
