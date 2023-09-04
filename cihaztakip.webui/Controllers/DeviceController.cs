using cihaztakip.business.Abstract;
using cihaztakip.business.Concrete;
using cihaztakip.entity;
using cihaztakip.entity.ViewModels;
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
        public async Task<IActionResult> DeviceList()
        {

            return View(await _deviceService.GetAllDevicesWithUserData());
        }

        public IActionResult CreateDevice()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDevice(DeviceModel model)
        {
            if (!ModelState.IsValid)//Validation and error handling
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = string.Join("\n", errors) });
            }

            await _deviceService.Create(new Device() { Name = model.Name });//create device

            return Json(new { success = true, redirectUrl = Url.Action("DeviceList") });//return
        
          
        }


        [HttpPost]
        public async Task<IActionResult> DeleteDevice(int deviceId)
        {
            var result = await _deviceService.Delete(deviceId);// delete device

            if (result.Succeeded)// delete is succesfull
            {
                return Json(new { success = true, message = $"{deviceId} Numaralı cihaz başarıyla silindi." });
            }
            //delete failed
            return Json(new { success = false, message = "Cihaz bulunamadı." });
        }


        public async Task<IActionResult> EditDevice(int id)
        {

            UserDeviceEditModel model = new UserDeviceEditModel();
            var device =await _deviceService.GetByIdWithUserDeviceData(id);

  
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
        public async Task<IActionResult> EditDevice(UserDeviceEditModel model)
        {
            if (ModelState.IsValid)
            {
              

                var device = await _deviceService.GetById(model.DeviceId);
                device.Name = model.DeviceName;
                await _deviceService.Update(device);

                return Json(new { success = true, redirectUrl = Url.Action("DeviceList") });
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return Json(new { success = false, message = "Validation failed", errors = errors });
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateUser(string email, int deviceId,int userdeviceId)
        {
            var user = _userManager.FindByEmailAsync(email).Result;

            if (user != null)
            {

                // userdeviceId returns  -1 if device has  no user

                if (userdeviceId >0)// there is userdevice  so update the user id 
                {
                    UserDevice userDevice = await _userDeviceService.GetById(userdeviceId);
                    UserDevice newuserdevice = new UserDevice() { UserId = user.Id, DeviceId = userDevice.DeviceId };
                    await _userDeviceService.Update(newuserdevice,userDevice);
                }
                else// there no userdevice so create new one
                {
                    UserDevice userDevice = new UserDevice() { DeviceId=deviceId,UserId=user.Id };
             
                   await  _userDeviceService.Create(userDevice);
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
        public async Task<IActionResult> DeleteUser(int deviceId, int userdeviceId)
        {
            var userdevice= await _userDeviceService.GetById(userdeviceId);
            if (userdevice != null)
            {
                await _userDeviceService.Delete(userdevice);

                return Json(new { success = true, redirectUrl = Url.Action("EditDevice", new { id = deviceId }) });
            }
            else
            {
                return Json(new { success = false, message = "Cihaza ait bir kullanıcı bulunmamaktadır!" });
            }
        }

    }
}
