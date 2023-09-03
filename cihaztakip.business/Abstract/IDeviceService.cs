using cihaztakip.entity;
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
        List<Device> GetAllWithUserData();
        void Update(Device device);
        void Create(Device device);
        void Delete(Device device);
        Device GetById(int id);
        Device GetByIdWithUserDeviceData(int id);
    }
}
