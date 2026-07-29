using GymSystem.DbContexts;
using GymSystem.Models;
using GymSystemBLL.Sevice.Classes;
using GymSystemBLL.Sevice.Interfaces;
using GymSystemBLL.ViewModels.PlanViewModel;
using GymSystemDAL.Repositories.Classes;
using GymSystemDAL.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymSystemPL.Controllers
{
    public class PlansController : Controller
    {
        private readonly IPlanService _planService;

        public PlansController(IPlanService planService)
        {
            _planService = planService;
        }

        //Get data
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var plans = await _planService.GetAllPlansAsync(ct: ct);
            return View(plans);
        }
        //Details
        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
            var plan = await _planService.GetPlanById(id, ct);
            if (plan == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        [HttpGet]
        public IActionResult Create(CreatePlanViewModel model, CancellationToken ct)
        { return View(model); }

        [HttpPost]
        public async Task<IActionResult> CreatePlan(CreatePlanViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(nameof(Create), model);
            var result = await _planService.CreatePlanAsync(model, ct);
            if (result)
                TempData["SuccessMessage"] = "Plan created successfully";
            else
                TempData["ErrorMessage"] = "Failed to create plan";
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var plan = await _planService.GetPlanById(id, ct);
            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan not found";
                return RedirectToAction(nameof(Index));
            }
            else
                return View(plan);
        }
        [HttpPost]
        public async Task<IActionResult> EditPlan(int id , PlanToUpdateViewModel model , CancellationToken ct)
        {
            if(!ModelState.IsValid) return View(model);
            var result = await _planService.UpdatePlanAsync(model ,id, ct);
            if(result) TempData["SuccessMessage"] = "Plan updated successfully";
            else
              TempData["ErrorMessage"] = "Failed to update";
              return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var plan = await _planService.GetPlanById(id, ct);
            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan not found";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed([FromRoute] int id, CancellationToken ct)
        {
            var result = await _planService.DeletePlanAsync(id, ct);
            if (result)
                TempData["SuccessMessage"] = "Plan deleted successfully";
            else
                TempData["ErrorMessage"] = "Failed to delete";
            return RedirectToAction(nameof(Index));
        }
    }
}
