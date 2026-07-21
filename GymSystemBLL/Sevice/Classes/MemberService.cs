using GymSystem.Models;
using GymSystemBLL.Sevice.Interfaces;
using GymSystemBLL.ViewModels.MemberViewModels;
<<<<<<< HEAD
<<<<<<< HEAD
using GymSystemDAL.Data.Models;
using GymSystemDAL.Models;
=======
>>>>>>> dev
=======

using GymSystemDAL.Data.Models;
using GymSystemDAL.Models;

>>>>>>> backup-before-merge
using GymSystemDAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
<<<<<<< HEAD
<<<<<<< HEAD
using System.Net.Sockets;
=======
>>>>>>> dev
=======
using System.Net.Sockets;

>>>>>>> backup-before-merge
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Sevice.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IGenaricRepository<Member> _memberRepository;
<<<<<<< HEAD
<<<<<<< HEAD
=======

>>>>>>> backup-before-merge
        private readonly IGenaricRepository<Membership> _membershipRepository;
        private readonly IGenaricRepository<Plan> _planRepository;

        public MemberService(IGenaricRepository<Member> memberRepository ,IGenaricRepository<Membership> MembershipRepository ,
            IGenaricRepository<Plan> PlanRepository)
        {
            _memberRepository = memberRepository;
            _membershipRepository = MembershipRepository;
            _planRepository = PlanRepository;
        }

        public async Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct = default)
        {
            //check mail exist or not
            var emailexists = await _memberRepository.AnyAsync(x => x.Email == model.Email, ct);
            //check phone exist or not
            var phoneexists = await _memberRepository.AnyAsync(x => x.Phone == model.Phone, ct);
            if (emailexists || phoneexists) return false;
            var member = new Member()
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                DataOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Address = new Address()
                {
                    BuildingNumber = model.BuildingNumber,
                    City = model.City,
                    Street = model.Street
                },
                HealthRecord = new HealthRecord()
                {
                    BloodType = model.HealthRecordViewModel.BloodType,
                    Note = model.HealthRecordViewModel.Note,
                    Weight = model.HealthRecordViewModel.Weight,
                    Height = model.HealthRecordViewModel.Height
                }

            };
            var result = await _memberRepository.AddAsync(member);
            return result > 0;
<<<<<<< HEAD
=======

        public MemberService(IGenaricRepository<Member> memberRepository)
        {
            _memberRepository = memberRepository;
>>>>>>> dev
=======
>>>>>>> backup-before-merge
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
<<<<<<< HEAD
<<<<<<< HEAD
=======

>>>>>>> backup-before-merge

        public async Task<MemberViewModel> GetMemberDetailsById(int memberid, CancellationToken ct)
        {
            var member = await _memberRepository.GetByIdAsync(memberid , ct);
            if (member == null) return null;
            var model = new MemberViewModel()
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                DateOfBirth = member.UpdatedAt.ToShortDateString(),
                Gender = member.Gender.ToString(),
                Address = $"{member.Address.Street }{member.Address.BuildingNumber}{member.Address.City}",
            };
            var activeMembership = await _membershipRepository.FirstOrDefaultAsync(x => x.MemberId == memberid && x.EndDate > DateTime.Now);
            if (activeMembership is not null)
            {
                var activePlan =await _planRepository.GetByIdAsync(activeMembership.PlanId);
                model.PlanName = activePlan.Name;
                model.MembershipStartDate = activeMembership.CreatedAt.ToShortDateString();
                model.MembershipEndDate = activeMembership.EndDate.ToShortDateString();
            }
            return model;
        }

<<<<<<< HEAD
=======
>>>>>>> dev
=======

>>>>>>> backup-before-merge
    }
}
