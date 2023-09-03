using cihaztakip.business.Abstract;
using cihaztakip.entity;
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
        private SignInManager<User> _signInManager;
        private IDeviceService _deviceService;
        public AccountController( UserManager<User> userManager, SignInManager<User> signInManager,IDeviceService deviceService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _deviceService = deviceService;

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
            if (!ModelState.IsValid)
            {

                var errors = ModelState.Values
             .SelectMany(v => v.Errors)
             .Select(e => e.ErrorMessage)
             .ToList();

                return Json(new { success = false, message = "Validation failed", errors = errors });

            }
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Bu kullanıcı adı ile bir kayıt yoktur");
                return Json(new { success = false, message = "Kullanıcı adı veya şifre hatalı!" });
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
            {

                return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
            }
            else
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

                return View(model);
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

                return RedirectToAction("Login", "Account");
            }
            ModelState.AddModelError("", "Bir Sıkıntı var tekrar dene.");
            return View();
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
