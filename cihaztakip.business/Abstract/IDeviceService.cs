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
        Task<List<Device>> GetAll();
        Task<DeviceListViewModel> GetAllDevicesWithUserData();
        Task<List<Device>> GetAllByUserId(string id);
        Task Update(Device device);
        Task Create(Device device);
        Task<Result> Delete(int deviceId);
        Task<Device> GetById(int id);
        Task<UserDeviceEditModel> GetDevicesByIdWithUserDeviceData(int id);

        Task<DeviceListViewModel> GetDevicesOfCurrentUser(string userId);
    }
}
