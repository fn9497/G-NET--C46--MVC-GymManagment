using GymSystem.Models;
using GymSystemBLL.Sevice.Interfaces;
using GymSystemBLL.ViewModels.MemberViewModels;
using GymSystemDAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Sevice.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IGenaricRepository<Member> _memberRepository;

        public MemberService(IGenaricRepository<Member> memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken ct = default)
        {
            var members = await _memberRepository.GetAllAsync(ct: ct);
            if (!members.Any()) return [];
            var memberViewModels = members.Select(m => new MemberViewModel()
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                Phone = m.Phone,
                Gender = m.Gender.ToString()
            });
            return memberViewModels;
        }
    }
}
