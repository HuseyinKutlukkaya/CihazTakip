using cihaztakip.business.Abstract;
using cihaztakip.business.Concrete;
using cihaztakip.data.Abstract;
using cihaztakip.data.Concrete.EfCore;
using cihaztakip.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cihaztakip.business.Concrete
{
    public class UserDeviceManager : IUserDeviceService
    {
        private readonly IUnitOfWork _unitofwork;
        public UserDeviceManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public void Create(UserDevice userdevice)
        {
            _unitofwork.UserDevices.Create(userdevice);
            _unitofwork.Save();
        }

        public void Delete(UserDevice userdevice)
        {
            _unitofwork.UserDevices.Delete(userdevice);
            _unitofwork.Save();
        }

        public UserDevice GetById(int id)
        {
            return _unitofwork.UserDevices.GetById(id);
        }

        public void Update(UserDevice userdevice,UserDevice oldUserDevice)
        {
            /*
             * Can't update the fk
             * _unitofwork.UserDevices.Update(userdevice);
           '*/
            Delete(oldUserDevice);
            Create(userdevice);
            
        }
    }
}
