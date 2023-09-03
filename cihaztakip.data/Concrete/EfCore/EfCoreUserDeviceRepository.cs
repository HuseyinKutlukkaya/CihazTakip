using cihaztakip.data.Abstract;
using cihaztakip.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cihaztakip.data.Concrete.EfCore
{
    public class EfCoreUserDeviceRepository: EfCoreGenericRepository<UserDevice>, IUserDeviceRepository
    {
        public EfCoreUserDeviceRepository(ApplicationDbContext ctx) : base(ctx)
        {

        }
        private ApplicationDbContext ApplicationDbContext
        {
            get { return context as ApplicationDbContext; }
        }

        public void DeleteAllUserData(string id)
        {
            var recordsToDelete = ApplicationDbContext.UserDevices
                                   .Where(x => x.UserId == id)
                                   .ToList(); 

           
            ApplicationDbContext.UserDevices.RemoveRange(recordsToDelete);
        }
    }

}
