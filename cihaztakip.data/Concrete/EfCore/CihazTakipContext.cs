using cihaztakip.entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cihaztakip.data.Concrete.EfCore
{
    public class CihazTakipContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Device> Devices { get; set; }


        public CihazTakipContext(DbContextOptions<CihazTakipContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.UseSerialColumns();
            base.OnModelCreating(modelBuilder);
        }
    }
}
