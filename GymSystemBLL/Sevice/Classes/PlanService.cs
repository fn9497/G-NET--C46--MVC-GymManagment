using GymSystem.Models;
using GymSystemBLL.Sevice.Interfaces;
using GymSystemBLL.ViewModels.PlanViewModel;
using GymSystemDAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Sevice.Classes
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<PlanViewModel>> GetAllPlansAsync(CancellationToken ct = default)
        {
            var plans = await _unitOfWork.GetRepository<Plan>().GetAllAsync();
            if (!plans.Any()) return [];
            var model = plans.Select(p=>new PlanViewModel()
            {
                Name = p.Name,
                Description = p.Description,
                IsActive =p.IsActive,
                Price = p.Price,
                DurationDays = p.DurationDays
            });
            return model;
        }

        public async Task<PlanViewModel> GetPlanById(int planId, CancellationToken ct = default)
        {
            var plan = await _unitOfWork.GetRepository<Plan>().GetByIdAsync(planId);
            if (plan == null) return null;
            var model = new PlanViewModel()
            {
                Name = plan.Name,
                Description = plan.Description,
                IsActive = plan.IsActive,
                Price = plan.Price,
                DurationDays = plan.DurationDays
            };
            return model;

        }
        public async Task<bool> CreatePlanAsync(CreatePlanViewModel model, CancellationToken ct = default)

        {
            var alreadyExistPlan = await _unitOfWork.GetRepository<Plan>().AnyAsync(p=>p.Name == model.Name && p.DurationDays ==model.DurationDays && p.Price ==model.Price);
            if (alreadyExistPlan) return false;
                var plan = new Plan()
                {
                    Name = model.Name,
                    Description = model.Description,
                    IsActive = model.IsActive,
                    Price = model.Price,
                    DurationDays = model.DurationDays
                };
            _unitOfWork.GetRepository<Plan>().AddAsync(plan);
            var result = await _unitOfWork.SaveChangesAsync();
            return result> 0;
        }

        public async Task<bool> DeletePlanAsync(int planId, CancellationToken ct = default)
        {
            var plan =await _unitOfWork.GetRepository<Plan>().GetByIdAsync(planId);
            if (plan == null) return false;
            if (plan.IsActive) return false;
            _unitOfWork.GetRepository<Plan>().DeleteAsync(plan);
            var result = await _unitOfWork.SaveChangesAsync();
            return result> 0;
        }

        public async Task<PlanToUpdateViewModel> GetPlanToUpdate(int planId, CancellationToken ct = default)
        {
            var plan = await _unitOfWork.GetRepository<Plan>().GetByIdAsync(planId, ct);
            if (plan == null) return null;
            else
            {
                var model = new PlanToUpdateViewModel
                {
                    Name = plan.Name,
                    Description = plan.Description,
                    IsActive = plan.IsActive,
                    Price = plan.Price,
                    DurationDays = plan.DurationDays
                };
                return model;
            }
        }

        public async Task<bool> UpdatePlanAsync(PlanToUpdateViewModel model, int planId, CancellationToken ct = default)
        {
            var plan = await _unitOfWork.GetRepository<Plan>().GetByIdAsync(planId);
            if (plan == null) return false;
            if (plan.IsActive) return false;
            else
            {
                plan.Name = model.Name;
                plan.Description = model.Description;
                plan.IsActive = model.IsActive;
                plan.Price = model.Price;
                plan.DurationDays = model.DurationDays;
                plan.UpdatedAt = DateTime.Now;
                _unitOfWork.GetRepository<Plan>().UpdateAsync(plan);
                var result = await _unitOfWork.SaveChangesAsync(ct);
                return result > 0;
            }
        }
    }
}
