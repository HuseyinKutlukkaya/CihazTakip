using cihaztakip.business.Abstract;
using cihaztakip.data.Abstract;
using cihaztakip.data.Concrete.EfCore;
using cihaztakip.entity;
using cihaztakip.entity.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace cihaztakip.business.Concrete
{
    public class IdentityManager : IIdentityService
    {
        private readonly IUnitOfWork _unitofwork;
        public IdentityManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public async Task<Result> Login(LoginModel model)
        {
            var user = await _unitofwork.UserManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return new Result { Succeeded=false };
           
            }
            var result = await _unitofwork.SignInManager.PasswordSignInAsync(user, model.Password, false, false);

            return new Result { Succeeded = result.Succeeded, Errors = result.ToString().Split(';').ToList() };
       
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
                    return new Result() { Succeeded = false,Errors=new List<string>() { "user Rolü atanamadı" } };
                }
                return new Result(){ Succeeded =true};
            }
            else// creation is failed
                return  new Result() { Succeeded = false, Errors = result.Errors.Select(e => e.Description).ToList() }; 



        }
    }
}
