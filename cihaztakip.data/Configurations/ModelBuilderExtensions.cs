using cihaztakip.entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cihaztakip.data.Configurations
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {

            builder.Entity<Device>().HasData(
                new Device() { DeviceId = 1, Name = "Telefon",  },
                new Device() { DeviceId = 2, Name = "Bilgisayar" },
                new Device() { DeviceId = 3, Name = "Elektronik" },
                new Device() { DeviceId = 4, Name = "Beyaz Eşya" }
            );

 
        }
    }
}
