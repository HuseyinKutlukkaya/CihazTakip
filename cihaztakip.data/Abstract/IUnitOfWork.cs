using cihaztakip.entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cihaztakip.data.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IDeviceRepository Devices { get; }
        IUserDeviceRepository UserDevices { get; }
        UserManager<User> UserManager { get; } 
        RoleManager<IdentityRole> RoleManager { get; } 
        SignInManager<User> SignInManager { get; } 

        void Save();
        Task<int> SaveAsync();

    }
}
