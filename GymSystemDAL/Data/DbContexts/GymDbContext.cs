using GymSystem.Configuration;
using GymSystem.Models;
using GymSystemDAL.Data.Models;
using GymSystemDAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymSystem.DbContexts
{
    public class GymDbContext :DbContext
    {
        public GymDbContext(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=GymSystem;Trusted_Connection=True;TrustServerCertificate=true");
        //}

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration<Plan>(new PlanConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Plan> Plans { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Trainer> Trainers { get; set; }


    }
}
