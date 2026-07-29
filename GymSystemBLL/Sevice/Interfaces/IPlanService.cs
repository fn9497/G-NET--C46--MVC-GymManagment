using GymSystemBLL.ViewModels.PlanViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Sevice.Interfaces
{
    public interface IPlanService
    {
        Task<IEnumerable<PlanViewModel>> GetAllPlansAsync(CancellationToken ct = default);
        Task<bool> CreatePlanAsync(CreatePlanViewModel model, CancellationToken ct = default);

        Task<PlanToUpdateViewModel> GetPlanToUpdate(int planId ,CancellationToken ct = default);
        Task<bool> UpdatePlanAsync(PlanToUpdateViewModel model , int planId , CancellationToken ct =default);

        Task<bool> DeletePlanAsync(int planId , CancellationToken ct = default);

        Task<PlanViewModel> GetPlanById(int planId, CancellationToken ct = default);

    }
}
