using GymSystemBLL.Common;
using GymSystemBLL.Sevice.Classes;
using GymSystemBLL.Sevice.Interfaces;
using GymSystemBLL.ViewModels.SessionViewModel;
using GymSystemBLL.ViewModels.TrainerViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace GymSystemPL.Controllers
{
    public class SessionsController : Controller
    {
        private readonly IsessionService _sessionService;

        public SessionsController(IsessionService sessionService)
        {
            _sessionService = sessionService;
        }
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var sessions = await _sessionService.GetAllSessionsAsync(ct);
            return View(sessions);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
           await PopulateDropListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSessionViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropListAsync();
                return View(model);
            }
                var result = await _sessionService.CreateSessionAscyn(model, ct);
            if (result.success)
            {
                TempData["SuccessMessage"] = "Sessions Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = result.error;
                await PopulateDropListAsync();
                return View(result);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id , CancellationToken ct)
        {
            var session = await _sessionService.GetSessionByIdAsync(id, ct);
            if(session is null)
            {
                TempData["ErrorMessage"] = "Session not found";
                return RedirectToAction(nameof(Index));
            }
            return View(session.value);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var session = await _sessionService.GetSessionToUpdate(id, ct);
            if (session is null)
            {
                TempData["ErrorMessage"] = "Session not found";
                return RedirectToAction(nameof(Index));
            }
            else
                return View(nameof(Edit), session);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateSessionViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropListAsync();
                return View(model); }

                var result = await _sessionService.UpdateSessionAsync(id, model, ct);
            if (result.success)
            {
                TempData["SuccessMessage"] = "Sessions updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = result.error;
                await PopulateDropListAsync();
                return View(result);
            }

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var session = await _sessionService.GetSessionByIdAsync(id, ct);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session not found";
                return RedirectToAction(nameof(Index));
            }
            return View(session.value);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed( int id, CancellationToken ct)
        {
            var result = await _sessionService.DeleteSessionAsync(id, ct);
            if (result.success)
                TempData["SuccessMessage"] = "Session deleted successfully";
            else
                TempData["ErrorMessage"] = "Session not found";
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropListAsync()
        {
            ViewBag.Trainers = new SelectList(await _sessionService.GetTrainerForDropDownAsync(),"Id","Name");
            ViewBag.Categories =new SelectList( await _sessionService.GetCategoryForDropDownAsync(),"Id","CategoryName");
        }
    }
}
