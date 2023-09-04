using cihaztakip.business.Abstract;
using cihaztakip.data.Abstract;
using cihaztakip.data.Concrete.EfCore;
using cihaztakip.entity.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<bool> Login(LoginModel model)
        {
            var user = await _unitofwork.UserManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return false;
           
            }
            var result = await _unitofwork.SignInManager.PasswordSignInAsync(user, model.Password, false, false);

            return result.Succeeded;
        }
    }
}
