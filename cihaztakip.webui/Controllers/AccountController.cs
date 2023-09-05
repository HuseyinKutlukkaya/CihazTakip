using cihaztakip.business.Abstract;
using cihaztakip.entity;
using cihaztakip.entity.ViewModels;
using cihaztakip.webui.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace cihaztakip.webui.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {

        private IDeviceService _deviceService;
        private IIdentityService _identityService;
        public AccountController( IDeviceService deviceService,IIdentityService identityService)
        {

            _deviceService = deviceService;

            _identityService = identityService;

        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Login(string? ReturnUrl = null)
        {
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)//Validation and error handling
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = string.Join("\n", errors) });
            }
            var result= await _identityService.Login(model);//Login
            if (result.Succeeded)//login succesfull
            {

                return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
            }
            else// login failed
                return Json(new { success = false, message = "Kullanıcı adı veya şifre hatalı!"});
           
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)//Validation and error handling
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = string.Join("\n", errors)});
            }
           var result=await _identityService.Register(model);//Register
           
            if (result.Succeeded)//register succesfull
            {
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
            }
            else// register failed
                return Json(new { success = false, message = string.Join("\n", result.Errors) });
        
        }


        public async Task<IActionResult> Logout()
        {
            await _identityService.LogOut();

            return Redirect("~/");
        }

        public async Task<IActionResult> Profile()
        {


            return View(await _deviceService.GetDevicesOfCurrentUser(User.Identity.GetUserId()));
        }
    }
}
