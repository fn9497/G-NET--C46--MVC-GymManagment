using GymSystem.Models;
using GymSystemDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Data.Configuration
{
    public class MemberConfigration : GymUserConfigration<Member>, IEntityTypeConfiguration<Member>
    {
        public new void Configure(EntityTypeBuilder<Member> builder)
        {

           builder.Property(x=>x.CreatedAt).HasColumnName("JoinDate").HasDefaultValueSql("getdate()");
           base.Configure(builder);
        }
    }
}
