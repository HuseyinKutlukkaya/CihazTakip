using cihaztakip.entity;
using cihaztakip.entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cihaztakip.business.Abstract
{
    public interface IDeviceService
    {
        List<Device> GetAll();
        Task<DeviceListViewModel> GetAllDevicesWithUserData();
        List<Device> GetAllByUserId(string id);
        void Update(Device device);
        Task Create(Device device);
        void Delete(Device device);
        Device GetById(int id);
        Device GetByIdWithUserDeviceData(int id);

        Task<DeviceListViewModel> GetDevicesOfCurrentUser(string userId);
    }
}
