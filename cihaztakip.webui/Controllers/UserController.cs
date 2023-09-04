using cihaztakip.entity;
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
        public UserController(UserManager<User> userManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult UserList()
        {
            //Get all the users
            List<User> users = _userManager.Users.ToList();

            //create userlistviewmodel with user and role 
            var userdata = new UserListViewModel
            {
                Users = users.Select(user => new UserData
                {
                    User = user,
                    Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault()
                }).ToList()
            };

           
            return View(userdata);
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

                        return Redirect("UserList");
                    }
                }
                return Redirect("UserList");
            }

            return View(model);

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
                return View(model);
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
                        return View(model);
                    }
                }

                return RedirectToAction("UserList");
            }

            ModelState.AddModelError("", "Bilinmeyen hata oldu lütfen tekrar deneyiniz.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserDelete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("UserList"); // Redirect to user list if the ID is empty or null.
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return RedirectToAction("UserList"); // Redirect to user list if the user is not found.
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("UserList"); // Redirect to user list after successful deletion.
            }
            else
            {
                // If there are errors in the deletion process, you can handle them here.
                // For example, you can add errors to ModelState and return the view.
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("UserList"); // Return to the user list view with error messages.
            }
        }


    }
}
