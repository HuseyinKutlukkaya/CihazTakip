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
        public async  Task Create(UserDevice userdevice)
        {
           await _unitofwork.UserDevices.CreateAsync(userdevice);
           await  _unitofwork.SaveAsync();
        }

        public async Task Delete(UserDevice userdevice)
        {
            await _unitofwork.UserDevices.DeleteAsync(userdevice);
            await _unitofwork.SaveAsync();
        }

        public async Task <UserDevice> GetById(int id)
        {
            return await _unitofwork.UserDevices.GetByIdAsync(id);
        }

        public async  Task Update(UserDevice userdevice,UserDevice oldUserDevice)
        {
            /*
             * Can't update the fk
             * _unitofwork.UserDevices.Update(userdevice);
           '*/
            await Delete(oldUserDevice);
            await  Create(userdevice);
            
        }
    }
}
