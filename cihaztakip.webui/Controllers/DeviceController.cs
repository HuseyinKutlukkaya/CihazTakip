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
        private IUserDeviceService _userDeviceService;
        public DeviceController(IDeviceService deviceService,IUserDeviceService userDeviceService)
        {
            _deviceService = deviceService;

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
        
            var model =await _deviceService.GetDevicesByIdWithUserDeviceData(id);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditDevice(UserDeviceEditModel model)
        {
            if (!ModelState.IsValid)//Validation and error handling
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = string.Join("\n", errors) });
            }

            var device = await _deviceService.GetById(model.DeviceId);//get device by id
            device.Name = model.DeviceName;// update the name 
            await _deviceService.Update(device);// update the device 

            return Json(new { success = true, redirectUrl = Url.Action("DeviceList") });
          
      
        }


        [HttpPost]
        public async Task<IActionResult> UpdateUser(string email, int deviceId,int userdeviceId)
        {

            var result =await  _userDeviceService.AddOrUpdateUserToDevice( email,  deviceId,  userdeviceId);// Update or add the user 
          

            if (result.Succeeded)// Success
            {
                return Json(new { success = true, redirectUrl = Url.Action("EditDevice", new { id = deviceId }) });
            }
            else//Fail
            {
                return Json(new { success = false, message = "Kullanıcı Bulunamadı!" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int deviceId, int userdeviceId)
        {
            var result= await _userDeviceService.DeleteUserFromDevice(userdeviceId, userdeviceId);//delete user from device 
            if (result.Succeeded)//Success
            {

                return Json(new { success = true, redirectUrl = Url.Action("EditDevice", new { id = deviceId }) });
            }
            else//Fail
            {
                return Json(new { success = false, message = "Cihaza ait bir kullanıcı bulunmamaktadır!" });
            }
        }

    }
}
