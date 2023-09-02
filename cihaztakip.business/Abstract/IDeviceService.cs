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
    }
}
