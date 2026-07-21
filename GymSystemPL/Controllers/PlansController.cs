using GymSystem.DbContexts;
using GymSystem.Models;
using GymSystemDAL.Repositories.Classes;
using GymSystemDAL.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymSystem.Controllers
{
    public class PlansController : Controller
    {
        private readonly IGenaricRepository<Plan> planRepository;

        public PlansController(IGenaricRepository<Plan> PlanRepository)
        {
            planRepository = PlanRepository;
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
