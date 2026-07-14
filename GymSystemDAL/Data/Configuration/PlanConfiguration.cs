using GymSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSystem.Configuration
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(p => p.Description).HasMaxLength(200);
            builder.Property(p => p.Price).HasPrecision(10,2);
            builder.Property(p=>p.UpdatedAt).HasDefaultValueSql("GETDATE()");

        }
    }
}
