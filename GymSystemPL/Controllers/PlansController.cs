using GymSystem.DbContexts;
using GymSystemDAL.Repositories.Classes;
using GymSystemDAL.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymSystem.Controllers
{
    public class PlansController : Controller
    {
        //private readonly GymDbContext dbContext;
        //public PlansController()
        //{
        //    dbContext = new GymDbContext();
        //}
        private readonly IplanRepository planRepository;

        public PlansController(IplanRepository _planRepository)
        {
            this.planRepository = _planRepository;
        }

        //Get data
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var plans = await planRepository.GetAllAsync(ct:ct);
            return View(plans);
        }
        //Details
        public async Task<IActionResult> Details(int id , CancellationToken ct)
        {
            var plan = await planRepository.GetByIdAsync(id , ct);
            if (plan == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }
    }
}
