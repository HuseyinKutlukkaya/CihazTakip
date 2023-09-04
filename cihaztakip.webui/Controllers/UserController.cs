using cihaztakip.business.Abstract;
using cihaztakip.entity;
using cihaztakip.entity.ViewModels;
using cihaztakip.webui.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task<IActionResult> UserList()
        {
            return View(await _identityService.GetAllUsersWithRoles());
        }
        public async Task<IActionResult> UserEdit(string id)
        {
            
            var userdetails = await _identityService.GetUserDetails(id);
            if (userdetails != null)
            {
                string Role =await  _identityService.GetRoleOfUser(id);//Get Role of the user

                //create SelectListItem List
                var roles =  _identityService.GetRoles().Result.Select(role => new SelectListItem
                {
                    Value = role.Name,
                    Text = role.Name
                }).ToList();

                foreach (var item in roles)//Select users role in the  SelectListItem List
                {
                    if (item.Value == Role)
                    {
                        item.Selected = true;
                        break; 
                    }
                }

                ViewBag.Roles = roles;
                return View(userdetails);
            }

            return RedirectToAction("UserList"); // If there is no user with the selected id
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserDetailsModel model)
        {
            if (!ModelState.IsValid)//Validation and error handling
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = string.Join("\n", errors) });
            }

            var result =await _identityService.UpdateUser(model);//update the user

            if (result.Succeeded)//successfull
            {
                return Json(new { success = true, redirectUrl = Url.Action("UserList") });
            }
            else//fail
            {
                return Json(new { success = false, message = "Kullanıcı güncellenirken bir hata oluştu." });

            }




        }
        public async Task<IActionResult> NewUser()
        {

            var roles = _identityService.GetRoles().Result
       .Select(role => new SelectListItem
       {
           Text = role.Name,
           Value = role.Name 
       })
       .ToList();

            ViewBag.Roles = roles;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewUser(NewUserModel model)
        {
            if (!ModelState.IsValid)//Validation and error handling
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = string.Join("\n", errors) });
            }

            var result =await _identityService.CreateNewUser(model);//Create new user

            if (result.Succeeded)//Sucessfull
            {
                result = await _identityService.AddRoleToUser(model.Email, model.Role);//add role

                if (result.Succeeded)//Sucessfull
                {
                    return Json(new { success = true, redirectUrl = Url.Action("UserList") });
                }
                else//fail
                {

                    return Json(new { success = false, message = "Seçilen rol geçersiz." });
                }

            }
            else//fail
                return Json(new { success = false, message = string.Join("\n", result.Errors) });

        }


        [HttpPost]
        public async Task<IActionResult> UserDelete(string userId)
        {
            var result=await _identityService.DeleteUserById(userId);//delete

            if (result.Succeeded)//succesfull
            {
                
                return Json(new { success = true, message = " Kullanıcı başarıyla silindi." });
            }
            else//fail
            {
            

                return Json(new { success = false, message = string.Join("\n", result.Errors) });
            }
        }

    }
}
