using cihaztakip.data.Abstract;
using cihaztakip.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cihaztakip.data.Concrete.EfCore
{
    public class EfCoreUserRepository: EfCoreGenericRepository<User>, IUserRepository
    {
        public EfCoreUserRepository(ApplicationDbContext ctx) : base(ctx)
        {
        }
        private ApplicationDbContext CihazTakipContext
        {
            get { return context as ApplicationDbContext; }
        }
    }
}
