using cihaztakip.business.Abstract;
using cihaztakip.entity;
using cihaztakip.entity.ViewModels;
using cihaztakip.webui.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cihaztakip.webui.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<User> _signInManager;
        private IDeviceService _deviceService;
        private IIdentityService _identityService;
        public AccountController( UserManager<User> userManager, SignInManager<User> signInManager,IDeviceService deviceService, RoleManager<IdentityRole> roleManager,IIdentityService identityService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _deviceService = deviceService;
            _roleManager = roleManager;
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
            bool result= await _identityService.Login(model);//Login
            if (result)//login succesfull
            {

                return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
            }
            else// login failed
                return Json(new { success = false, message = "Kullanıcı adı veya şifre hatalı!" });
           
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                          .SelectMany(v => v.Errors)
                          .Select(e => e.ErrorMessage)
                          .ToList();

                return Json(new { success = false, message = string.Join("\n", errors),errors= errors });
            }
            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                string role = "user";
                var roleExists = await _roleManager.RoleExistsAsync(role);

                if (roleExists)
                {
                    // Assign the user to the selected role
                    await _userManager.AddToRoleAsync(user, role);
                }
                return Json(new { success = true, redirectUrl = Url.Action("Login", "Account") });
            }
            ModelState.AddModelError("", "Bir hata oluştu. Lütfen tekrar deneyin.");
            return Json(new { success = false, message = "Kayıt başarısız.", errors=result.Errors });
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        public IActionResult Profile()
        {
            DeviceListViewModel list = new DeviceListViewModel();
            string userId = _userManager.GetUserId(User);
            list.Devices = _deviceService.GetAllByUserId(userId);

            return View(list);
        }
    }
}
