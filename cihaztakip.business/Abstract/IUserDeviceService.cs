using cihaztakip.entity;
using cihaztakip.entity.ViewModels;
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
        Task<Result> DeleteUserFromDevice(int DeviceId, int userdeviceId);
        Task<UserDevice> GetById(int id);
        Task<Result> AddOrUpdateUserToDevice(string email, int deviceId, int userdeviceId); 
    }
}
