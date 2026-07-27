using GymSystem.Models;
using GymSystemBLL.Sevice.Interfaces;
using GymSystemBLL.ViewModels.MemberViewModels;
using GymSystemDAL.Data.Models;
using GymSystemDAL.Models;
using GymSystemDAL.Data.Models;
using GymSystemDAL.Models;

using GymSystemDAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Sevice.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IGenaricRepository<Member> _memberRepository;
        private readonly IGenaricRepository<Membership> _membershipRepository;
        private readonly IGenaricRepository<Plan> _planRepository;
        private readonly IGenaricRepository<HealthRecord> _healthRepository;
        private readonly IGenaricRepository<Booking> _bookinRepository;

        public MemberService(IGenaricRepository<Member> memberRepository ,IGenaricRepository<Membership> MembershipRepository ,
            IGenaricRepository<Plan> PlanRepository , IGenaricRepository<HealthRecord> HealthRepository , IGenaricRepository<Booking> BookinRepository)
        {
            _memberRepository = memberRepository;
            _membershipRepository = MembershipRepository;
            _planRepository = PlanRepository;
            _healthRepository = HealthRepository;
            _bookinRepository = BookinRepository;
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
        }

        public async Task<bool> DeleteMemberAsync(int memberid, CancellationToken ct = default)
        {
            var member = await _memberRepository.GetByIdAsync(memberid,ct);
            if (member == null) return false;
            var hasFutureBooking = await _bookinRepository.AnyAsync(x => x.MemberId == memberid && x.Session.StartDate > DateTime.Now, ct);
            if (hasFutureBooking) return false;
            var result = await _memberRepository.DeleteAsync(member);
            return result > 0;
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

        public async Task<HealthRecordViewModel> GetHealthRecordDetails(int memberid, CancellationToken ct = default)
        {
            var record = await _healthRepository.FirstOrDefaultAsync(m => m.memberId == memberid);
            if (record == null) return null;
            else
            {
                return new HealthRecordViewModel()
                {
                    Weight = record.Weight,
                    Height = record.Height,
                    BloodType = record.BloodType,
                    Note = record.Note
                };
            }
        }

        public async Task<MemberViewModel> GetMemberDetailsById(int memberid, CancellationToken ct)
        {
            var member = await _memberRepository.GetByIdAsync(memberid , ct);
            if (member == null) return null;
            var model = new MemberViewModel()
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                DateOfBirth = member.DataOfBirth.ToShortDateString(),
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

        public async Task<MemberToUpdateViewModel?> GetMemberToUpdate(int memberid, CancellationToken ct = default)
        {
           var member = await _memberRepository.GetByIdAsync(memberid , ct);
            if (member == null) return null;
            else
            {
                return new MemberToUpdateViewModel()
                {
                    Name = member.Name,
                    Email = member.Email,
                    Phone = member.Phone,
                    BuildingNumber = member.Address.BuildingNumber,
                    City = member.Address.City,
                    Street = member.Address.Street,
                    Photo = member.Photo
                };
            }
        }

        public async Task<bool> UpdateMemberDetails(int id, MemberToUpdateViewModel model, CancellationToken ct = default)
        {
            var member = await _memberRepository.GetByIdAsync (id , ct);
            if (member == null) return false;
            else
            {
                var emailexists = await _memberRepository.AnyAsync(m => m.Email == model.Email && m.Id != id);
                var phoneexists = await _memberRepository.AnyAsync(m => m.Phone == m.Phone && m.Id != id);
                if (emailexists || phoneexists) return false;
                member.Email = model.Email;
                member.Phone = model.Phone;
                member.Address.City = model.City;
                member.Address.Street = model.Street;
                member.Address.BuildingNumber = model.BuildingNumber;
                member.UpdatedAt = DateTime.Now;
                var result = await _memberRepository.UpdateAsync(member);
                return result > 0;
            }
        }
    }
}
