using cihaztakip.business.Abstract;
using cihaztakip.data.Abstract;
using cihaztakip.data.Concrete.EfCore;
using cihaztakip.entity;
using cihaztakip.entity.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace cihaztakip.business.Concrete
{
    public class IdentityManager : IIdentityService
    {
        private readonly IUnitOfWork _unitofwork;
        public IdentityManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task<UserListViewModel> GetAllUsersWithRoles()
        {

            List<User> users = _unitofwork.UserManager.Users.ToList(); //Get all the users

            var userdata = new UserListViewModel//Create user x role list
            {
                Users = users.Select(user => new UserData
                {
                    User = user,
                    Role = _unitofwork.UserManager.GetRolesAsync(user).Result.FirstOrDefault()// get role of the user
                }).ToList()
            };
            return userdata;
        }

        public async Task<string> GetRoleOfUser(string id)
        {
            var user = await _unitofwork.UserManager.FindByIdAsync(id);

            return _unitofwork.UserManager.GetRolesAsync(user).Result.FirstOrDefault().ToString();
        }

        public async Task<List<IdentityRole>> GetRoles()
        {
            return  _unitofwork.RoleManager.Roles.ToList();
        }

        public async Task<UserDetailsModel> GetUserDetails(string id)
        {

            var user = await _unitofwork.UserManager.FindByIdAsync(id);
            if (user != null)
            {
                return new UserDetailsModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = await GetRoleOfUser(id)
                };
            }

            return null;
        }

        public async Task<Result> Login(LoginModel model)
        {
            var user = await _unitofwork.UserManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return new Result { Succeeded = false };

            }
            var result = await _unitofwork.SignInManager.PasswordSignInAsync(user, model.Password, false, false);

            return new Result { Succeeded = result.Succeeded, Errors = result.ToString().Split(';').ToList() };

        }

        public async Task LogOut()
        {
            await _unitofwork.SignInManager.SignOutAsync();//logout


        }

        public async Task<Result> Register(RegisterModel model)
        {
            var user = new User()//create a new user entity
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
            };
            var result = await _unitofwork.UserManager.CreateAsync(user, model.Password);//create new user

            if (result.Succeeded)// creation is successfull
            {
                string role = "user";//default role
                var roleExists = await _unitofwork.RoleManager.RoleExistsAsync(role);//Checking role exist

                if (roleExists)// role exist
                {
                    // Assign the user to the selected role
                    await _unitofwork.UserManager.AddToRoleAsync(user, role);// add new role to user
                }
                else//role is not exist
                {
                    return new Result() { Succeeded = false, Errors = new List<string>() { "user Rolü atanamadı" } };
                }
                return new Result() { Succeeded = true };
            }
            else// creation is failed
                return new Result() { Succeeded = false, Errors = result.Errors.Select(e => e.Description).ToList() };



        }

        public async Task<Result> UpdateUser(UserDetailsModel model)
        {
            var user = await _unitofwork.UserManager.FindByIdAsync(model.UserId);

            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserName = model.UserName;
                user.Email = model.Email;


                var result = await _unitofwork.UserManager.UpdateAsync(user);//update the user data

                if (result.Succeeded)//if update is successfull then update the role
                {
                    var userRoles = await _unitofwork.UserManager.GetRolesAsync(user);

                    await _unitofwork.UserManager.RemoveFromRolesAsync(user, userRoles.ToArray<string>());//delete existing roles
                    await _unitofwork.UserManager.AddToRoleAsync(user, model.Role);//add new role

                    return new Result() { Succeeded = true };
                }
            }
            return new Result() { Succeeded = false };
        }

        public async Task<Result> CreateNewUser(NewUserModel model)
        {

            var user = new User()//create user entity
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _unitofwork.UserManager.CreateAsync(user, model.Password);//create user

            return new Result() { Succeeded = result.Succeeded, Errors = result.Errors.Select(e => e.Description).ToList() };
        }
        public async Task<Result> AddRoleToUser(string email, string role)
        {

            var user = await _unitofwork.UserManager.FindByEmailAsync(email);

            if (user == null) { return new Result() { Succeeded = false }; }

            // Check if the selected role is not null and not empty
            if (!string.IsNullOrEmpty(role))
            {
                // Check if the role exists
                var roleExists = await _unitofwork.RoleManager.RoleExistsAsync(role);

                if (roleExists)
                {
                    // Assign the user to the selected role
                    await _unitofwork.UserManager.AddToRoleAsync(user, role);
                    return new Result() { Succeeded = true };
                }
                else
                {

                    return new Result() { Succeeded = false };
                }
            }
            return new Result() { Succeeded = false };
        }
        public async Task<Result> DeleteUserById(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new Result() { Succeeded = false };
            }

            var user = await _unitofwork.UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new Result() { Succeeded = false }; // Redirect to user list if the user is not found.
            }

            var result = await _unitofwork.UserManager.DeleteAsync(user);

            return new Result() { Succeeded = result.Succeeded, Errors = result.Errors.Select(e => e.Description).ToList() };
        }
    }
}
