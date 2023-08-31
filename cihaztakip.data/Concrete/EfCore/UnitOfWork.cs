using cihaztakip.data.Abstract;
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
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        private EfCoreDeviceRepository _deviceRepository;
        private EfCoreUserRepository _userRepository;
        public IDeviceRepository Devices =>
             _deviceRepository = _deviceRepository ?? new EfCoreDeviceRepository(_context);

        public IUserRepository Users =>
             _userRepository = _userRepository ?? new EfCoreUserRepository(_context);
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
