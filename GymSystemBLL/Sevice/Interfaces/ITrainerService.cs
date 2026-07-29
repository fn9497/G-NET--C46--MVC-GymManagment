using GymSystemBLL.ViewModels.TrainerViewModel;
using GymSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Sevice.Interfaces
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerViewModel>> GetAllTrainersAsync(CancellationToken ct = default);
        Task<TrainerViewModel> GetTrainerDetailsById(int trainerId, CancellationToken ct = default);
        Task<bool> CreateTrainerAsync(CreateTrainerViewModel model, CancellationToken ct=default);
        Task<TrainerToUpdateViewModel> GetTrainerToUpdate(int trainerId, CancellationToken ct = default);
        Task<bool> UpdateTrainerAsync(int trainerId , TrainerToUpdateViewModel model , CancellationToken ct = default);
        Task<bool> DeleteTrainerAsync(int TrainerId , CancellationToken ct =default);
    }
}
