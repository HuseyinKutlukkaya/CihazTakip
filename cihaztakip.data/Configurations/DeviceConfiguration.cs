using cihaztakip.entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cihaztakip.data.Configurations
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder) 
        {
            builder.HasKey(x => x.DeviceId);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            builder.HasOne(d => d.User)
                   .WithMany(u => u.Devices)
                   .HasForeignKey(d => d.UserId);
        }
    }
}
