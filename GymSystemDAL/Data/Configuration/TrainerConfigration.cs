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
    public class TrainerConfigration : GymUserConfigration<Trainer>, IEntityTypeConfiguration<Trainer>
    {
        public new void Configure(EntityTypeBuilder<Trainer> builder)
        {

            builder.Property(x => x.CreatedAt).HasColumnName("HireDate").HasDefaultValueSql("getdate()");
            base.Configure(builder);
        }
    }
}
