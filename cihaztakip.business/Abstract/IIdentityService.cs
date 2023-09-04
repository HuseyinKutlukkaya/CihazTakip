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
        public  Task<bool> Login(LoginModel model);
 
    }
}
