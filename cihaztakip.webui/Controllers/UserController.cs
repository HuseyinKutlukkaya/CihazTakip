using cihaztakip.entity;
using cihaztakip.webui.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cihaztakip.webui.Controllers
{
    public class UserController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

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
    }
}
