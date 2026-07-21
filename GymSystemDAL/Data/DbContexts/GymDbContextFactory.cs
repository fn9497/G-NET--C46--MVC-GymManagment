using GymSystem.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GymSystemDAL.Data.DbContexts
{
    public class GymDbContextFactory : IDesignTimeDbContextFactory<GymDbContext>
    {
        public GymDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<GymDbContext>()
                .UseSqlServer("Server=.;Database=GymSystem;Trusted_Connection=True;TrustServerCertificate=True")
                .Options;

            return new GymDbContext(options);
        }
    }
}