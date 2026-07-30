using GymSystemBLL.Common;
using GymSystemBLL.ViewModels.SessionViewModel;
using GymSystemBLL.ViewModels.TrainerViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Sevice.Interfaces
{
    public interface IsessionService
    {
        Task<IEnumerable<SessionViewModel>?>GetAllSessionsAsync(CancellationToken ct=default);
        Task<Result<SessionViewModel>?> GetSessionByIdAsync(int sessionId , CancellationToken ct);
        Task<Result> CreateSessionAscyn(CreateSessionViewModel model , CancellationToken ct=default);
        Task<IEnumerable<TrainerSelectViewModel>> GetTrainerForDropDownAsync(CancellationToken ct = default);
        Task<IEnumerable<CategorySelectViewModel>> GetCategoryForDropDownAsync(CancellationToken ct = default);
        Task<UpdateSessionViewModel> GetSessionToUpdate(int sessionId, CancellationToken ct = default);
        Task<Result> UpdateSessionAsync(int sessionId, UpdateSessionViewModel model, CancellationToken ct = default);
        Task<Result> DeleteSessionAsync(int sessionId, CancellationToken ct = default);
    }
}
