using cihaztakip.data.Abstract;
using cihaztakip.entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cihaztakip.data.Concrete.EfCore
{
    public class EfCoreDeviceRepository : EfCoreGenericRepository<Device>, IDeviceRepository
    {
        public EfCoreDeviceRepository(ApplicationDbContext ctx) : base(ctx)
        {

        }
        private ApplicationDbContext ApplicationDbContext
        {
            get { return context as ApplicationDbContext; }
        }

 

        public List<Device> GetAllWithUserData()
        {
            var devices = ApplicationDbContext.
                Devices.
                Include(i=>i.UserDevices).
                ThenInclude(i=>i.User).
                ToList();
            


            return devices;
        }

        public Device GetByIdWithUserDeviceData(int id)
        {
            return ApplicationDbContext.Devices.Where(i => i.DeviceId == id).
                Include(i => i.UserDevices).
                ThenInclude(i => i.User).
                FirstOrDefault();
        }
    }
}
