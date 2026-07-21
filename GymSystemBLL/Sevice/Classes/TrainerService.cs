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
        private readonly IGenaricRepository<Trainer> _trainerRepository;

        public TrainerService(IGenaricRepository<Trainer> trainerRepository)
        {
            _trainerRepository = trainerRepository;
        }
        public async Task<bool> CreateTrainerAsync(CreateTrainerViewModel model, CancellationToken ct=default)
        {
            var emailExists = await _trainerRepository.AnyAsync(x=>x.Email == model.Email,ct);
            var phoneExist = await _trainerRepository.AnyAsync(x=>x.Phone == model.Phone,ct);
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
            var result =await _trainerRepository.AddAsync(trainer);
            return result > 0;


        }

        public async Task<IEnumerable<TrainerViewModel>> GetAllTrainersAsync(CancellationToken ct = default)
        {
            var Trainers = await _trainerRepository.GetAllAsync();
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
    }
}
