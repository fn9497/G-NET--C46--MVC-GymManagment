using GymSystemBLL.Sevice.Interfaces;
using GymSystemBLL.ViewModels.TrainerViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymSystemPL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TrainersController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainersController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }
        public async Task<IActionResult> Index()
        {
            var trainers = await _trainerService.GetAllTrainersAsync();
            return View(trainers);
        }
        public async Task<IActionResult> TrainerDetails(int id, CancellationToken ct)
        { 
            var trainer = await _trainerService.GetTrainerDetailsById(id , ct);
            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer not found";
                return RedirectToAction(nameof(Index));
            }
            return View("Details",trainer);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTrainerViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid) return View(nameof(Create), model);

            var result = await _trainerService.CreateTrainerAsync(model, ct);
            if (result)
                TempData["SuccessMessage"] = "Trainer created successfully.";
            else
                TempData["ErrorMessage"] = "Failed to create member.";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> EditTrainer(int id,CancellationToken ct)
        { 
            var trainer = await _trainerService.GetTrainerToUpdate(id , ct);
            if(trainer is null)
            { 
                TempData["ErrorMessage"] = "Trainer not found";
               return RedirectToAction(nameof(Index));
            }
            else
            return View(nameof(EditTrainer),trainer);
        }
        [HttpPost]
        public async Task<IActionResult> EditTrainer(int id , TrainerToUpdateViewModel model,CancellationToken ct)
        {
            if (!ModelState.IsValid) return View(model);
            else
            {
                var result = await _trainerService.UpdateTrainerAsync(id, model, ct);
                if(result)
                TempData["SuccessMessage"] = "Trainer updated successfully";
                else
                    TempData["ErrorMessage"] = "Failed to update";
                return RedirectToAction(nameof(Index));
            }

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id , CancellationToken ct)
        {
            var trainer =await _trainerService.GetTrainerDetailsById(id , ct);
            if(trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer not found";
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed([FromBody] int id , CancellationToken ct)
        {
            var result = await _trainerService.DeleteTrainerAsync(id , ct);
            if(result)
                TempData["SuccessMessage"] = "Trainer deleted successfully";
            else
                TempData["ErrorMessage"] = "Trainer not found";
            return RedirectToAction(nameof(Index));
        }
    }
}
