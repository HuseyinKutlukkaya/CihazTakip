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
        public async Task<List<Device>> GetAll()
        {
            return await _unitofwork.Devices.GetAll();
        }
    }
}
