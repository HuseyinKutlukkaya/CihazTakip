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
         void Update(UserDevice userdevice, UserDevice oldUserDevice);
         void Create(UserDevice userdevice);
        void Delete(UserDevice userdevice);
        UserDevice GetById(int id);
    }
}
