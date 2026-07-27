using GymSystem.Models;
using GymSystemBLL.ViewModels.MemberViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Sevice.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken ct = default);

        Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct = default);

        Task<MemberViewModel?> GetMemberDetailsById(int memberid, CancellationToken ct = default);

        Task<HealthRecordViewModel?> GetHealthRecordDetails(int memberid ,CancellationToken ct = default);

        Task<MemberToUpdateViewModel?> GetMemberToUpdate(int memberid, CancellationToken ct = default);

        Task <bool>UpdateMemberDetails( int memberid , MemberToUpdateViewModel model, CancellationToken ct = default);

        Task<bool> DeleteMemberAsync(int memberid, CancellationToken ct = default);
    }
}
