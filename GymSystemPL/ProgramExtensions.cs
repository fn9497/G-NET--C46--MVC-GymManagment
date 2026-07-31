using GymSystem;
using GymSystem.DbContexts;
using GymSystemDAL.Data.DataSeeding;
using GymSystemDAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymSystemPL
{
    public static class ProgramExtensions
    {
        public static async Task MigrateAndSeedDataAsync(this WebApplication app)
        {
            using var Scope = app.Services.CreateScope();
            var dbcontext = Scope.ServiceProvider.GetRequiredService<GymDbContext>();
            var logger = Scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
             var Rolemanager = Scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
             var userManager = Scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
             var pendingmogration = await dbcontext.Database.GetPendingMigrationsAsync();
          
            if (pendingmogration != null)

            {
                await dbcontext.Database.MigrateAsync();
            }
            var seedfolderpath = Path.Combine(app.Environment.ContentRootPath, "wwwroot", "Files");
            await GymDataSeeding.SeedAsync(dbcontext, seedfolderpath, logger);
            await IdentityDataSeeding.SeedAsync(Rolemanager, userManager, logger);
        }

    }
}
