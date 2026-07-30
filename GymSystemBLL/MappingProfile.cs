using AutoMapper;
using AutoMapper.Execution;
using GymSystem.Models;
using GymSystemBLL.ViewModels.MemberViewModels;
using GymSystemBLL.ViewModels.PlanViewModel;
using GymSystemBLL.ViewModels.SessionViewModel;
using GymSystemBLL.ViewModels.TrainerViewModel;
using GymSystemDAL.Models;
using GymSystemDAL.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapMember();
            MapSession();
            MapPlan();
            MapTrainer();

        }
        private void MapMember()
        {
            CreateMap<GymSystem.Models.Member, MemberViewModel>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber}-{src.Address.Street}-{src.Address.City}"))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DataOfBirth.ToShortDateString()));
            CreateMap<HealthRecord, HealthRecordViewModel>();
            CreateMap<GymSystem.Models.Member, MemberToUpdateViewModel>()
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));

            CreateMap<MemberToUpdateViewModel, GymSystem.Models.Member>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.City = src.City;
                    dest.Address.Street = src.Street;
                });
            CreateMap<CreateMemberViewModel, GymSystem.Models.Member>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City
                }))
                .ForMember(dest => dest.HealthRecord, opt => opt.MapFrom(src => new HealthRecord
                {
                    Height = src.HealthRecordViewModel.Height,
                    Weight = src.HealthRecordViewModel.Weight,
                    Note = src.HealthRecordViewModel.Note,
                    BloodType = src.HealthRecordViewModel.BloodType
                }));

        }
        private void MapSession() 
        {
            CreateMap<Session, SessionViewModel>()
                .ForMember(dest=>dest.TrainerName , opt=>opt.MapFrom(src=> src.Trainer.Name))
                .ForMember(dest=>dest.CategoryName , opt=>opt.MapFrom(src=> src.Category.CategoryName));
            CreateMap<CreateSessionViewModel, Session>();
            CreateMap<Trainer, TrainerSelectViewModel>();
            CreateMap<Category, CategorySelectViewModel>();
        }
        private void MapPlan()
        {
            CreateMap<Plan, PlanViewModel>();

            CreateMap<CreatePlanViewModel, Plan>();

            CreateMap<Plan, PlanToUpdateViewModel>();

            CreateMap<PlanToUpdateViewModel, Plan>();
        }
        private void MapTrainer()
        {
            CreateMap<Trainer, TrainerViewModel>()
    .ForMember(dest => dest.Address,
        opt => opt.MapFrom(src =>
            $"{src.Address.BuildingNumber} - {src.Address.Street} - {src.Address.City}"))
    .ForMember(dest => dest.DateOfBirth,
        opt => opt.MapFrom(src => src.DataOfBirth.ToShortDateString()))
    .ForMember(dest => dest.Specialities,
        opt => opt.MapFrom(src => new List<Speciality> { src.Speciality }))
    .ForMember(dest => dest.Sessions,
        opt => opt.MapFrom(src => src.Sessions));

            CreateMap<Trainer, TrainerToUpdateViewModel>()
                .ForMember(dest => dest.BuildingNumber,
                    opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.City,
                    opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Street,
                    opt => opt.MapFrom(src => src.Address.Street));

            CreateMap<TrainerToUpdateViewModel, Trainer>()
                .ForMember(dest => dest.Address,
                    opt => opt.MapFrom(src => new Address
                    {
                        BuildingNumber = src.BuildingNumber,
                        City = src.City,
                        Street = src.Street
                    }));

            CreateMap<CreateTrainerViewModel, Trainer>()
                .ForMember(dest => dest.Address,
                    opt => opt.MapFrom(src => new Address
                    {
                        BuildingNumber = src.BuildingNumber,
                        City = src.City,
                        Street = src.Street
                    }));
        }

    }
}
