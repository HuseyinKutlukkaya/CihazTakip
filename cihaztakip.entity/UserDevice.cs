using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cihaztakip.entity
{
    public class UserDevice
    {
        public int UserDeviceId { get; set; }
        public string UserId { get; set; }
        public int DeviceId { get; set; }

        public User User { get; set; }
        public Device Device { get; set; }
    }
}
