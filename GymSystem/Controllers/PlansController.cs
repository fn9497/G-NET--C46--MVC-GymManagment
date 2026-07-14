using GymSystem.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymSystem.Controllers
{
    public class PlansController : Controller
    {
        private readonly GymDbContext dbContext;
        public PlansController()
        {
            dbContext = new GymDbContext();
        }

        //Get data
        public async Task<IActionResult> Index()
        {
            var plans = await dbContext.Plans.ToListAsync();
            return View(plans);
        }
        //Details
        public async Task<IActionResult> Details(int id)
        {
            var plan = await dbContext.Plans.FindAsync(id);
            if (plan == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }
    }
}
