using GymSystem.DbContexts;
using GymSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymSystemDAL.Data.DataSeeding
{
    public static class GymDataSeeding
    {
        public static async Task SeedAsync(GymDbContext dbContext,String SeedFolderpath ,ILogger logger, CancellationToken ct=default )
        {
            try 
            {
                if(!await dbContext.Plans.AnyAsync(ct))
                {
                    var plans = LoadDataFromJsonFile<Plan>(SeedFolderpath , "plans.json");
                    if(plans.Any())
                    {
                        dbContext.Plans.AddRange(plans);
                    }
                    if (dbContext.ChangeTracker.HasChanges())
                        await dbContext.SaveChangesAsync();
                    else
                        logger.LogInformation("Plan already seeded");

                }
            
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Seed data failed");
                throw;
            }
        }


        private static List<T> LoadDataFromJsonFile<T>(string folderpath , string Filename)
        {

            var filepath = Path.Combine(folderpath, Filename);
            if (!File.Exists(filepath))
                throw new FileNotFoundException($"seeddata file not found :{filepath}");
            var data = File.ReadAllText(filepath);
            var option = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<T>>(data, option)??[];


        }

    }
}
