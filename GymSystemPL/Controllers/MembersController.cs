using GymSystem.Models;
using GymSystemBLL.Sevice.Classes;
using GymSystemBLL.Sevice.Interfaces;
using GymSystemBLL.ViewModels.MemberViewModels;
using GymSystemDAL.Repositories.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GymSystemPL.Controllers
{
    public class MembersController : Controller
    {
       private readonly IMemberService _memberService;
        public MembersController(IMemberService memberService)
        {
           _memberService = memberService;
        }
        public async Task<IActionResult> Index()
        {
            var members = await _memberService.GetAllMembersAsync();
            return View(members);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View(nameof(Create));
        }
        [HttpPost]
        public async Task<IActionResult> CreateMember(CreateMemberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Create), model);
            }
            var result = await _memberService.CreateMemberAsync(model);
            if (result)
                TempData["SuccessMessage"] = "Member created successfully.";
            else
                TempData["ErrorMessage"] = "Failed to create member.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> MemberDetails(int id, CancellationToken ct)
        {
            //get by id
            var member = await _memberService.GetMemberDetailsById(id, ct);
            if (member == null)
            {
                TempData["ErrorMessage"] = "Member not found";
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        public async Task<IActionResult> HealthRecordDetails(int id, CancellationToken ct)
        { 
            var result = await _memberService.GetHealthRecordDetails(id, ct);
            if (result == null)
            {
                TempData["ErrorMessage"] = "Health record not found";
                return RedirectToAction(nameof(Index));
            }
            else return View(result);


        }
        [HttpGet]
        public async Task<IActionResult> EditMember(int id, CancellationToken ct)
        {
            var member = await _memberService.GetMemberToUpdate(id, ct);
            if (member is null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }
            else 
                return View(member);
        }
        [HttpPost]
        public async Task<IActionResult> EditMember(int id, MemberToUpdateViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid) return View(model);
            else {
                var result = await _memberService.UpdateMemberDetails(id, model, ct);
                if (result)
                    TempData["SuccessMessage"] = "Member Updated Successfully";
                else
                    TempData["ErrorMessage"] = "Failed to update";
                   return RedirectToAction(nameof(Index));               
                        
                        } 
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        { 
            var member = await _memberService.GetMemberDetailsById(id, ct);
            if (member == null)
            {
                TempData["ErrorMessage"] = "Member not found";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed([FromRoute] int id, CancellationToken ct)
        { 
            var result = await _memberService.DeleteMemberAsync(id, ct);
            if (result)
                TempData["SuccessMessage"] = "Member deleted successfully";
            else
                TempData["ErrorMessage"] = "Failed to delete";
            return RedirectToAction(nameof(Index));
        }

    }
}
