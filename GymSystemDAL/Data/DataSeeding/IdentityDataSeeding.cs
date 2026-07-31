using GymSystemDAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Data.DataSeeding
{
    public static class IdentityDataSeeding
    {

        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,ILogger logger,CancellationToken ct = default)
        {
            try
            {
                bool hasUsers = userManager.Users.Any();
                bool hasRoles = roleManager.Roles.Any();

                if (hasUsers && hasRoles) return;

                if (!hasRoles)
                {
                    var roles = new List<IdentityRole>()
                    {
                        new IdentityRole() { Name = "SuperAdmin" },
                        new IdentityRole() { Name = "Admin" }
                    };

                    foreach (var roleName in roles.Select(r => r.Name))
                    {
                        if (!await roleManager.RoleExistsAsync(roleName!))
                        {
                            var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));

                            if (!roleResult.Succeeded)
                            {
                                logger.LogError("Failed to create Role {Role}: {Errors}", roleName,
                                    string.Join(", ",roleResult.Errors.Select(e => e.Description)));
                            }
                        }
                    }
                }

                if (!hasUsers)
                {
                    var mainAdmin = new ApplicationUser()
                    {
                        firstName = "Mariam",
                        lastName = "Ali",
                        UserName = "MariamAli",
                        Email = "MariamaAli@gmail.com",
                        PhoneNumber = "01123652635"
                    };

                    await userManager.CreateAsync(mainAdmin, "P@ssw0rd");
                    await userManager.AddToRoleAsync(mainAdmin, "SuperAdmin");

                    var admin01 = new ApplicationUser()
                    {
                        firstName = "Omar",
                        lastName = "Mohamed",
                        UserName = "OmarMohamed",
                        Email = "OmarMohamed@gmail.com",
                        PhoneNumber = "01232589652"
                    };

                    var createResult =await userManager.CreateAsync(admin01, "P@ssw0rd");

                    if (!createResult.Succeeded)
                    {
                        logger.LogError("Failed to create seed SuperAdmin: {Errors}",string.Join(", ",createResult.Errors.Select(e => e.Description)));
                        return;
                    }

                    logger.LogInformation($"Seeded SuperAdmin {admin01.Email}");
                }

                return;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Identity seeding failed.");
                throw;
            }
        }
    
}
}
