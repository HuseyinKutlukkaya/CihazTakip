using cihaztakip.business.Abstract;
using cihaztakip.data.Abstract;
using cihaztakip.entity;
using cihaztakip.entity.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace cihaztakip.business.Concrete
{
    public class DeviceManager:IDeviceService
    {
        private readonly IUnitOfWork _unitofwork;
        public DeviceManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task Create(Device device)
        {
           await _unitofwork.Devices.CreateAsync(device);

            await _unitofwork.SaveAsync(); 
        }

        public async Task<List<Device>> GetAll()
        {
            return await _unitofwork.Devices.GetAllAsync();
        }

        public async Task<DeviceListViewModel> GetAllDevicesWithUserData()
        {
            DeviceListViewModel list = new DeviceListViewModel();
            list.Devices =await _unitofwork.Devices.GetAllDevicesWithUserData();

            return list;
        }


        public async Task<Result> Delete(int deviceId)
        {

            var device = await GetById(deviceId);//Check if device exist

            if (device != null)// if device exist
            {
                await _unitofwork.Devices.DeleteAsync(device);
                await _unitofwork.SaveAsync();
                return new Result() { Succeeded=true };
            }
            else//device is not exist
                return new Result() { Succeeded = false };


        }

        public async Task<Device> GetById(int id)
        {
           return await _unitofwork.Devices.GetByIdAsync(id);
        }

        public async Task<UserDeviceEditModel> GetDevicesByIdWithUserDeviceData(int id)
        {

            UserDeviceEditModel model = new UserDeviceEditModel();
            var device = await _unitofwork.Devices.GetByIdWithUserDeviceData(id);
            model.DeviceId = device.DeviceId;
            model.DeviceName = device.Name;
            if (device.UserDevices.Count > 0)
            {
                model.UserDeviceId = device.UserDevices[0].UserDeviceId;
                model.UserId = device.UserDevices[0].UserId;
                model.UserName = device.UserDevices[0].User.UserName;
                model.FirstName = device.UserDevices[0].User.FirstName;
                model.LastName = device.UserDevices[0].User.LastName;
                model.Email = device.UserDevices[0].User.Email;

                var user = await _unitofwork.UserManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var roles = await _unitofwork.UserManager.GetRolesAsync(user);
                    model.Role = roles.FirstOrDefault();
                }

            }
            return model;
        }

        public async Task Update(Device device)
        {
            await _unitofwork.Devices.UpdateAsync(device);
            await _unitofwork.SaveAsync();
        }

        public async Task<List<Device>> GetAllByUserId(string id )
        {
            return await _unitofwork.Devices.GetAllByUserId(id);
        }

        public async Task<DeviceListViewModel> GetDevicesOfCurrentUser(string userId)
        {
            DeviceListViewModel list = new DeviceListViewModel();
            list.Devices = await GetAllByUserId(userId);

            return list;
        }
    }
}
