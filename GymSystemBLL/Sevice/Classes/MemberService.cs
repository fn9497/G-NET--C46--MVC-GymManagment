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
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GymSystemBLL.Sevice.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAttachmentService _attachmentService;

        public MemberService( IUnitOfWork unitOfWork , IMapper mapper , IAttachmentService attachmentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _attachmentService = attachmentService;
        }

        public async Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct = default)
        {
            //check mail exist or not
            var emailexists = await _unitOfWork.GetRepository<Member>().AnyAsync(x => x.Email == model.Email, ct);
            //check phone exist or not
            var phoneexists = await _unitOfWork.GetRepository<Member>().AnyAsync(x => x.Phone == model.Phone, ct);
            if (emailexists || phoneexists) return false;
            var member = _mapper.Map<CreateMemberViewModel,Member>(model);
            var newphotoname = await _attachmentService.UploadAsync(model.PhotoFile.OpenReadStream(), model.PhotoFile.FileName, "MemberPicture", ct);
            if (string.IsNullOrEmpty(newphotoname)) return false;
            member.Photo = newphotoname;
            _unitOfWork.GetRepository<Member>().AddAsync(member);

            var result =await _unitOfWork.SaveChangesAsync(ct);
            return result > 0;
        }

        public async Task<bool> DeleteMemberAsync(int memberid, CancellationToken ct = default)
        {
            var member = await _unitOfWork.GetRepository<Member>().GetByIdAsync(memberid,ct);
            if (member == null) return false;
            var hasFutureBooking = await _unitOfWork.GetRepository<Booking>().AnyAsync(x => x.MemberId == memberid && x.Session.StartDate > DateTime.Now, ct);
            if (hasFutureBooking) return false;
             _unitOfWork.GetRepository<Member>().DeleteAsync(member);
           if(member.Photo is not null)
            {
                _attachmentService.Delete(member.Photo, "MemberPicture");
            }

            var result =await _unitOfWork.SaveChangesAsync(ct);
            return result > 0;
        }

        public async Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken ct = default)
        {
            var members = await _unitOfWork.GetRepository<Member>().GetAllAsync(ct: ct);
            if (!members.Any()) return [];
            var memberViewModels = _mapper.Map<IEnumerable<Member>, IEnumerable<MemberViewModel>>(members); 
            return memberViewModels;
        }

        public async Task<HealthRecordViewModel> GetHealthRecordDetails(int memberid, CancellationToken ct = default)
        {
            var record = await _unitOfWork.GetRepository<HealthRecord>().FirstOrDefaultAsync(m => m.memberId == memberid);
            if (record == null) return null;
            else
            {
                var model = _mapper.Map<HealthRecord, HealthRecordViewModel>(record);
                return model;
            }
        }

        public async Task<MemberViewModel> GetMemberDetailsById(int memberid, CancellationToken ct)
        {
            var member = await _unitOfWork.GetRepository<Member>().GetByIdAsync(memberid , ct);
            if (member == null) return null;
            var model = _mapper.Map<Member,MemberViewModel>(member);
            var activeMembership = await _unitOfWork.GetRepository<Membership>().FirstOrDefaultAsync(x => x.MemberId == memberid && x.EndDate > DateTime.Now);
            if (activeMembership is not null)
            {
                var activePlan =await _unitOfWork.GetRepository<Plan>().GetByIdAsync(activeMembership.PlanId);
                model.PlanName = activePlan.Name;
                model.MembershipStartDate = activeMembership.CreatedAt.ToShortDateString();
                model.MembershipEndDate = activeMembership.EndDate.ToShortDateString();
            }
            return model;
        }

        public async Task<MemberToUpdateViewModel?> GetMemberToUpdate(int memberid, CancellationToken ct = default)
        {
           var member = await _unitOfWork.GetRepository<Member>().GetByIdAsync(memberid , ct);
            if (member == null) return null;
            else
            {
                return _mapper.Map<Member, MemberToUpdateViewModel>(member);
            }
        }

        public async Task<bool> UpdateMemberDetails(int id, MemberToUpdateViewModel model, CancellationToken ct = default)
        {
            var member = await _unitOfWork.GetRepository<Member>().GetByIdAsync (id , ct);
            if (member == null) return false;
            else
            {
                var emailexists = await _unitOfWork.GetRepository<Member>().AnyAsync(m => m.Email == model.Email && m.Id != id);
                var phoneexists = await _unitOfWork.GetRepository<Member>().AnyAsync(m => m.Phone == m.Phone && m.Id != id);
                if (emailexists || phoneexists) return false;
                _mapper.Map(model ,member);
                member.UpdatedAt = DateTime.Now;
                 _unitOfWork.GetRepository<Member>().UpdateAsync(member);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0;
            }
        }
    }
}
