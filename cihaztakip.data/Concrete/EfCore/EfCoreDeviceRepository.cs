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
        public EfCoreDeviceRepository(CihazTakipContext ctx) : base(ctx)
        {
        }
        private CihazTakipContext CihazTakipContext
        {
            get { return context as CihazTakipContext; }
        }
    }
}
