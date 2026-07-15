using GymSystem.Models;
using GymSystemBLL.Sevice.Classes;
using GymSystemBLL.Sevice.Interfaces;
using GymSystemDAL.Repositories.Interface;
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
           var  members = await _memberService.GetAllMembersAsync();
           return View(members);
        }
    }
}
