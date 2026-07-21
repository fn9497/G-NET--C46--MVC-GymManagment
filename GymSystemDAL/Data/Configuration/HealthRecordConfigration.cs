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
    public class HealthRecordConfigration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.Property(x => x.BloodType).HasMaxLength(4);
            builder.Property(x => x.Note).HasMaxLength(500);
        }
    }
}
