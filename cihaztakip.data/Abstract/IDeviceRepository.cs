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
        List<Device> GetAllDevicesWithUserData();
        Device GetByIdWithUserDeviceData(int id);
        List<Device> GetAllByUserId(string id);
    }
}
