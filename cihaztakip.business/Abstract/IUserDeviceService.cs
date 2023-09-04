using cihaztakip.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cihaztakip.business.Abstract
{
    public interface IUserDeviceService
    {
         Task Update(UserDevice userdevice, UserDevice oldUserDevice);
        Task Create(UserDevice userdevice);
        Task Delete(UserDevice userdevice);
        Task<UserDevice> GetById(int id);
    }
}
