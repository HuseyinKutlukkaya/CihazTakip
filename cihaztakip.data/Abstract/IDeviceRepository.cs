using cihaztakip.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cihaztakip.data.Abstract
{
    public interface IDeviceRepository : IRepository<Device> 
    {
        Task<List<Device>> GetAllDevicesWithUserData();
        Task<Device> GetByIdWithUserDeviceData(int id);
        Task<List<Device>> GetAllByUserId(string id);
    }
}
