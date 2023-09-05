using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cihaztakip.entity
{
    public class Device
    {
        public int DeviceId { get; set; }
        public string Name { get; set; }

        public List<UserDevice> UserDevices { get; set; }

    }
}
