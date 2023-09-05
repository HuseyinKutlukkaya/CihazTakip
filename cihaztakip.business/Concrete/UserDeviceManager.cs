using cihaztakip.business.Abstract;
using cihaztakip.business.Concrete;
using cihaztakip.data.Abstract;
using cihaztakip.data.Concrete.EfCore;
using cihaztakip.entity;
using cihaztakip.entity.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

        public async Task<Result> AddOrUpdateUserToDevice(string email, int deviceId, int userdeviceId)
        {
            var user = _unitofwork.UserManager.FindByEmailAsync(email).Result;

            if (user != null)
            {

                // userdeviceId returns  -1 if device has  no user

                if (userdeviceId > 0)// there is userdevice  so update the user id 
                {
                    UserDevice userDevice = await GetById(userdeviceId);
                    UserDevice newuserdevice = new UserDevice() { UserId = user.Id, DeviceId = userDevice.DeviceId };
                    await Update(newuserdevice, userDevice);
                }
                else// there no userdevice so create new one
                {
                    UserDevice userDevice = new UserDevice() { DeviceId = deviceId, UserId = user.Id };

                    await Create(userDevice);
                }
                return new Result() { Succeeded = true };

            }


            return  new Result() { Succeeded = false };

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

        public async Task<Result> DeleteUserFromDevice(int DeviceId, int userdeviceId)
        {

            var userdevice = await GetById(userdeviceId);
            if (userdevice != null)
            {
                await  Delete(userdevice);

                return new Result() { Succeeded = true };
            }
            else
            {
                return new Result() { Succeeded = false };
            }

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
