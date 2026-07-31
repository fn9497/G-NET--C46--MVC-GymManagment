using GymSystemBLL.Sevice.Interfaces;
using GymSystemBLL.ViewModels.AnalyticsViewModel;
using GymSystemDAL.Models;
using GymSystem.Models;
using GymSystemDAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemDAL.Data.Models;

namespace GymSystemBLL.Sevice.Classes
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnalyticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<AnalyticsViewModel> GetAnalyticsDataAsync(CancellationToken ct=default)
        {
            var sessions = await _unitOfWork.GetRepository<Session>().GetAllAsync();
            var totalMember = await _unitOfWork.GetRepository<Member>().CountAsync(ct:ct);
            var totalTrainer = await _unitOfWork.GetRepository<Trainer>().CountAsync(ct:ct);
            var activeMmeber = await _unitOfWork.GetRepository<Membership>().CountAsync(m => m.EndDate > DateTime.Now);

            return new AnalyticsViewModel
            {
                TotalMembers = totalMember,
                TotalTrainers = totalTrainer,
                ActiveMembers = activeMmeber,
                UpcomingSessions = sessions.Count(s => s.StartDate > DateTime.Now),
                OngoingSessions = sessions.Count(s => s.StartDate <= DateTime.Now && s.EndDate >= DateTime.Now),
                CompletedSessions = sessions.Count(s => s.EndDate < DateTime.Now)
            };
        }
    }
}
