using GymSystem.DbContexts;
using GymSystemBLL;
using GymSystemBLL.Sevice.Classes;
using GymSystemBLL.Sevice.Interfaces;
using GymSystemDAL.Repositories.Classes;
using GymSystemDAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace GymSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped(typeof(IGenaricRepository<>) ,typeof(GenaricRepository<>));
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<ITrainerService, TrainerService>();
            builder.Services.AddScoped<IPlanService, PlanService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddScoped<IsessionService, SessionService>();
            builder.Services.AddDbContext<GymDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddAutoMapper(m=> m.AddProfile (new MappingProfile()));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
