using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cihaztakip.entity;

namespace cihaztakip.data.Configurations
{
    public class UserDeviceConfiguration : IEntityTypeConfiguration<UserDevice>
    {
        public void Configure(EntityTypeBuilder<UserDevice> builder)
        {
            builder.HasKey(ud => ud.UserDeviceId);
            builder.HasAlternateKey(ud => new { ud.UserId, ud.DeviceId });

     
            builder.HasOne(ud => ud.User)
                   .WithMany(u => u.UserDevices)
                   .HasForeignKey(ud => ud.UserId);

           
            builder.HasOne(ud => ud.Device)
                   .WithMany(d => d.UserDevices)
                   .HasForeignKey(ud => ud.DeviceId);

        }
    }
}
