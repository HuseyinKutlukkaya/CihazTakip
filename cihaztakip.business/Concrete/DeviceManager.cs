using cihaztakip.business.Abstract;
using cihaztakip.data.Abstract;
using cihaztakip.entity;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Create(Device device)
        {
            _unitofwork.Devices.Create(device);
            _unitofwork.Save(); 
        }

        public List<Device> GetAll()
        {
            return  _unitofwork.Devices.GetAll();
        }

        public List<Device> GetAllWithUserData()
        {
            return _unitofwork.Devices.GetAllWithUserData();
        }
    }
}
