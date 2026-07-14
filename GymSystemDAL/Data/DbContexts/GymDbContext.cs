using GymSystem.Configuration;
using GymSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace GymSystem.DbContexts
{
    public class GymDbContext :DbContext
    {

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=GymSystem;Trusted_Connection=True;TrustServerCertificate=true");
        //}

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Plan>(new PlanConfiguration());
        }

        public DbSet<Models.Plan> Plans { get; set; }

    }
}
