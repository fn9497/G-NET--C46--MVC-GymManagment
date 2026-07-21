using GymSystem.Models;
using GymSystemDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Data.Configuration
{
    public class GymUserConfigration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x=>x.Name).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x=>x.Email).HasColumnType("varchar").HasMaxLength(100);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Phone).IsUnique();
            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("PhoneCheck",
                    "Phone LIKE '011%' or Phone Like '012%' or Phone Like '010%' or Phone Like '015%'");

                tb.HasCheckConstraint("EmailCheck",
                    "Email LIKE '%_@_%._%'");
            });
            builder.OwnsOne(x => x.Address, address =>
            {
                address.Property(x => x.City).HasColumnType("varchar").HasMaxLength(30);
                address.Property(x => x.Street).HasColumnType("varchar").HasMaxLength(30);
            });

        }

       
    }
}
