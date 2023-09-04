using cihaztakip.business.Abstract;
using cihaztakip.entity;
using cihaztakip.entity.ViewModels.cihaztakip.entity.ViewModels;
using cihaztakip.webui.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace cihaztakip.webui.Controllers
{
    public class UserController : Controller
    {
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IIdentityService _identityService;
        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IIdentityService identityService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _identityService = identityService;
        }
        public IActionResult UserList()
        {
            return View(_identityService.GetAllUsersWithRoles());
        }
        public async Task<IActionResult> UserEdit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                string Role =  _userManager.GetRolesAsync(user).Result.FirstOrDefault();
                var roles = _roleManager.Roles.Select(role => new SelectListItem
                {
                    Value = role.Name,
                    Text = role.Name
                }).ToList();

                foreach (var item in roles)
                {
                    if (item.Value == Role)
                    {
                        item.Selected = true;
                        break; 
                    }
                }

                ViewBag.Roles = roles;
                return View(new UserDetailsModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = Role
                });
            }

            return RedirectToAction("UserList"); // If there is no user with the selected id
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserDetailsModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.UserName = model.UserName;
                    user.Email = model.Email;
         

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);

                        await _userManager.RemoveFromRolesAsync(user, userRoles.ToArray<string>());//delete existing roles
                        await _userManager.AddToRoleAsync(user, model.Role);//add new role

                        return Json(new { success = true, redirectUrl = Url.Action("UserList") });
                    }
                }
                
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors)
             .Select(e => e.ErrorMessage);

            return Json(new { success = false, message = "Validation failed", errors = errors });

        }
        public IActionResult NewUser()
        {

            var roles = _roleManager.Roles.Select(role => new SelectListItem
            {
                Value = role.Name,
                Text = role.Name
            }).ToList();

            ViewBag.Roles = roles;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewUser(NewUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Form validation failed." });
            }

            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Check if the selected role is not null and not empty
                if (!string.IsNullOrEmpty(model.Role))
                {
                    // Check if the role exists
                    var roleExists = await _roleManager.RoleExistsAsync(model.Role);

                    if (roleExists)
                    {
                        // Assign the user to the selected role
                        await _userManager.AddToRoleAsync(user, model.Role);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Seçilen rol geçersiz.");
                        return Json(new { success = false, message = "Seçilen rol geçersiz." });
                    }
                }

                return Json(new { success = true, redirectUrl = Url.Action("UserList") });
            }

            var errors = result.Errors.Select(e => e.Description).ToList();
            ModelState.AddModelError("", "Bilinmeyen hata oldu lütfen tekrar deneyiniz.");
            return Json(new { success = false, message = string.Join(" ", errors) });
        }


        [HttpPost]
        public async Task<IActionResult> UserDelete(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("UserList"); // Redirect to user list if the ID is empty or null.
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return RedirectToAction("UserList"); // Redirect to user list if the user is not found.
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                // Return a JSON response indicating success
                return Json(new { success = true, message = " Kullanıcı başarıyla silindi." });
            }
            else
            {
            
                var errors = result.Errors.Select(error => error.Description);
                return Json(new { success = false, errors = errors });
            }
        }

    }
}
