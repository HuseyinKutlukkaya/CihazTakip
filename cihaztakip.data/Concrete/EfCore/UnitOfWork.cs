using cihaztakip.data.Abstract;
using cihaztakip.entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cihaztakip.data.Concrete.EfCore
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public UnitOfWork(
            ApplicationDbContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        private EfCoreDeviceRepository _deviceRepository;
        private EfCoreUserDeviceRepository _userRepository;
        public IDeviceRepository Devices =>
             _deviceRepository = _deviceRepository ?? new EfCoreDeviceRepository(_context);

        public IUserDeviceRepository UserDevices =>
             _userRepository = _userRepository ?? new EfCoreUserDeviceRepository(_context);
        
        public UserManager<User> UserManager => _userManager;

        
        public RoleManager<IdentityRole> RoleManager => _roleManager;

        
        public SignInManager<User> SignInManager => _signInManager;

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
