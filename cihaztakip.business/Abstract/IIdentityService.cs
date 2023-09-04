using cihaztakip.entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cihaztakip.business.Abstract
{
    public interface IIdentityService
    {
        public  Task<Result> Login(LoginModel model);
        public  Task<Result> Register(RegisterModel model);
    }
}
