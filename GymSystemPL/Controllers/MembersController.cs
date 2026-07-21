using GymSystem.Models;
using GymSystemBLL.Sevice.Classes;
using GymSystemBLL.Sevice.Interfaces;
<<<<<<< HEAD
using GymSystemBLL.ViewModels.MemberViewModels;
=======
>>>>>>> dev
using GymSystemDAL.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GymSystemPL.Controllers
{
    public class MembersController : Controller
    {
<<<<<<< HEAD
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
=======
       private readonly IMemberService _memberService;
        public MembersController(IMemberService memberService)
        {
           _memberService = memberService;
>>>>>>> dev
        }
        public async Task<IActionResult> Index()
        {
           var  members = await _memberService.GetAllMembersAsync();
           return View(members);
        }
<<<<<<< HEAD
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateMember(CreateMemberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Create),model);
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
            var member =await _memberService.GetMemberDetailsById(id ,ct);
            if (member == null)
            {
                TempData["ErrorMessage"] = "Member not found";
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }


=======
>>>>>>> dev
    }
}
