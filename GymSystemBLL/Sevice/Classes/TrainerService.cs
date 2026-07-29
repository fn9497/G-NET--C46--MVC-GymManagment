using GymSystemBLL.Sevice.Interfaces;
using GymSystemBLL.ViewModels.TrainerViewModel;
using GymSystemDAL.Models;
using GymSystemDAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Sevice.Classes
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TrainerViewModel>> GetAllTrainersAsync(CancellationToken ct = default)
        {
            var Trainers = await _unitOfWork.GetRepository<Trainer>().GetAllAsync();
            if (!Trainers.Any()) return [];
            var trainerViewModel = Trainers.Select(x => new TrainerViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Email=x.Email,
                Phone = x.Phone,
                Gender = x.Gender.ToString()
            });
            return trainerViewModel;
        }
     
        public async Task<TrainerViewModel> GetTrainerDetailsById(int trainerId, CancellationToken ct = default)
        {
            var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(trainerId , ct);
            if (trainer == null) return null;
            var model = new TrainerViewModel()
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Gender = trainer.Gender.ToString(),
                DateOfBirth = trainer.DataOfBirth.ToShortDateString(),
                Address =trainer.Address.BuildingNumber +" "+ trainer.Address.Street + " "+trainer.Address.City,  
            };
            return model;
 //*********************************************************later after session viewModel creation
            var Sessions = await _unitOfWork.GetRepository<Session>().GetAllAsync();
            if (!Sessions.Any(x => x.TrainerId == trainerId))
                model.Sessions=null;
            else
            {
              
            }
                    }
        public async Task<bool> CreateTrainerAsync(CreateTrainerViewModel model, CancellationToken ct=default)
        {
            var emailExists = await _unitOfWork.GetRepository<Trainer>().AnyAsync(x=>x.Email == model.Email,ct);
            var phoneExist = await _unitOfWork.GetRepository<Trainer>().AnyAsync(x=>x.Phone == model.Phone,ct);
            if (emailExists || phoneExist) return false;
            var trainer = new Trainer()
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Gender = model.Gender,

                Address = new Address()
                {
                    BuildingNumber = model.BuildingNumber,
                    City = model.City,
                    Street = model.Street
                }
            };
            _unitOfWork.GetRepository<Trainer>().AddAsync(trainer);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;


        }
        public async Task<TrainerToUpdateViewModel> GetTrainerToUpdate(int trainerId, CancellationToken ct = default)
        {
            var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(trainerId, ct);
            if (trainer == null) return null;
            var model = new TrainerToUpdateViewModel()
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Street = trainer.Address.Street,
                BuildingNumber = trainer.Address.BuildingNumber,
                City= trainer.Address.City,
                Speciality=trainer.Speciality
            };
            return model;
        }
        public async Task<bool> UpdateTrainerAsync(int trainerId, TrainerToUpdateViewModel model, CancellationToken ct = default)
        {
            var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(trainerId);
            if (trainer == null) return false;
            else 
            {
                var emailExists = await _unitOfWork.GetRepository<Trainer>().AnyAsync(x=>x.Id !=trainerId && x.Email == model.Email);
                var phoneExists = await _unitOfWork.GetRepository<Trainer>().AnyAsync(x=>x.Id !=trainerId && x.Phone == model.Phone);
                if (emailExists || phoneExists) return false;
                else
                { 
                    trainer.Email = model.Email;
                    trainer.Phone = model.Phone;
                    trainer.UpdatedAt = DateTime.Now;
                    _unitOfWork.GetRepository<Trainer>().UpdateAsync(trainer);
                    var result = await _unitOfWork.SaveChangesAsync();
                    return result > 0;
                }
            }
            
        }
       public async Task<bool> DeleteTrainerAsync(int TrainerId, CancellationToken ct = default)
            {
               var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(TrainerId);
                if(trainer == null) return false;
                if (trainer.Sessions.Any()) return false;
                else
                {
                    _unitOfWork.GetRepository<Trainer>().DeleteAsync(trainer);
                    var result = await _unitOfWork.SaveChangesAsync(ct);
                    return result > 0;
                }
            }
    }
}
