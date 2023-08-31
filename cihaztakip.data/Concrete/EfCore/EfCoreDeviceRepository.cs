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
        private ApplicationDbContext CihazTakipContext
        {
            get { return context as ApplicationDbContext; }
        }
    }
}
