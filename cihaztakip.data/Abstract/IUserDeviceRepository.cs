using cihaztakip.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cihaztakip.data.Abstract
{
    public interface IUserDeviceRepository:IRepository<UserDevice>
    {
        public void DeleteAllUserData(string id);
    }
}
