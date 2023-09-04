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

        public async Task<Device> GetByIdWithUserDeviceData(int id)
        {
           return await _unitofwork.Devices.GetByIdWithUserDeviceData(id);
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
