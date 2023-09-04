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
            _unitofwork.Devices.Create(device);

            await _unitofwork.SaveAsync(); 
        }

        public List<Device> GetAll()
        {
            return  _unitofwork.Devices.GetAll();
        }

        public async Task<DeviceListViewModel> GetAllDevicesWithUserData()
        {
            DeviceListViewModel list = new DeviceListViewModel();
            list.Devices = _unitofwork.Devices.GetAllDevicesWithUserData();

            return list;
        }


        public void Delete(Device device)
        {
            _unitofwork.Devices.Delete(device);
            _unitofwork.Save();
        }

        public Device GetById(int id)
        {
           return _unitofwork.Devices.GetById(id);
        }

        public Device GetByIdWithUserDeviceData(int id)
        {
           return _unitofwork.Devices.GetByIdWithUserDeviceData(id);
        }

        public void Update(Device device)
        {
            _unitofwork.Devices.Update(device);
            _unitofwork.Save();
        }

        public List<Device> GetAllByUserId(string id )
        {
            return _unitofwork.Devices.GetAllByUserId(id);
        }

        public async Task<DeviceListViewModel> GetDevicesOfCurrentUser(string userId)
        {
            DeviceListViewModel list = new DeviceListViewModel();
            list.Devices = GetAllByUserId(userId);

            return list;
        }
    }
}
