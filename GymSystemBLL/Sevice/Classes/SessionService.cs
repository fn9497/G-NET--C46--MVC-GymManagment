using AutoMapper;
using GymSystem.Models;
using GymSystemBLL.Common;
using GymSystemBLL.Sevice.Interfaces;
using GymSystemBLL.ViewModels.PlanViewModel;
using GymSystemBLL.ViewModels.SessionViewModel;
using GymSystemDAL.Models;
using GymSystemDAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Sevice.Classes
{
    public class SessionService : IsessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> CreateSessionAscyn(CreateSessionViewModel model, CancellationToken ct)
        {
            if (model.EndDate <= model.StartDate) return Result.Validation("End date must be after start date");
            if (model.StartDate <= DateTime.Now) return Result.Validation("StartDate must be in the future");
            if (model.Capacity < 1 || model.Capacity > 25) return Result.Validation("Capasity must be between 1 and 25");
            var trainer = await _unitOfWork.GetRepository<Trainer>().GetAllAsync();
            if(trainer is null) return Result.NotFound("Trainer not found");
            var category = await _unitOfWork.GetRepository<Category>().GetAllAsync();
            if (category is null) return Result.NotFound("Category not found");
            var session = _mapper.Map<CreateSessionViewModel, Session>(model);
            _unitOfWork.GetRepository<Session>().AddAsync(session);
            var result = await _unitOfWork.SaveChangesAsync();
            return result>0 ? Result.Ok() :Result.Fail("Failed to create session");

        }

        public async Task<Result> DeleteSessionAsync(int sessionId, CancellationToken ct = default)
        {
            var session = await _unitOfWork.SessionRepository.GetByIdAsync(sessionId);
            if (session == null) return Result.NotFound("Session not found");
            if (session.EndDate > DateTime.Now) return Result.Validation("Session is ongoing can't be deleted");
            _unitOfWork.SessionRepository.DeleteAsync(session);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0 ?Result.Ok():Result.Fail("Failed to delete");
        }

        public async Task<IEnumerable<SessionViewModel>?> GetAllSessionsAsync(CancellationToken ct)
        {
            var sessions = await _unitOfWork.SessionRepository.GetAllSessionWithTrainersAndCategories(ct);
            if (sessions == null && sessions.Any()) return null;
                var mappedSessions = sessions.Select(s => new SessionViewModel()
                {
                    Id = s.Id,
                    Capacity = s.Capacity,
                    CategoryName = s.Category.CategoryName,
                    TrainerName = s.Trainer.Name,
                    Description = s.Description,
                    EndDate = s.EndDate,
                    StartDate = s.StartDate
                });
                foreach (var session in mappedSessions)
                {
                    session.AvailableSlots = session.Capacity - await _unitOfWork.SessionRepository.GetCountOfBookSlots(session.Id, ct);
                }
                return mappedSessions;
            }

        public async Task<IEnumerable<CategorySelectViewModel>> GetCategoryForDropDownAsync(CancellationToken ct = default)
        {
            var result = await _unitOfWork.GetRepository<Category>().GetAllAsync(ct:ct);
            return _mapper.Map<IEnumerable<CategorySelectViewModel>>(result);
        }

        public async Task<Result<SessionViewModel>?> GetSessionByIdAsync(int sessionId, CancellationToken ct)
        {
            var session = await _unitOfWork.SessionRepository.GetSessionWithTrainerAndCategory(sessionId, ct);
            if (session is null)
                return Result<SessionViewModel>.NotFound("Session not found");
            else
            {
                var mappedSession = _mapper.Map<Session, SessionViewModel>(session);
                mappedSession.AvailableSlots = mappedSession.Capacity - await _unitOfWork.SessionRepository.GetCountOfBookSlots(sessionId);
                return Result<SessionViewModel>.Ok(mappedSession);
                    }
        }

        public async Task<UpdateSessionViewModel> GetSessionToUpdate(int sessionId, CancellationToken ct = default)
        {
            var session = await _unitOfWork.SessionRepository.GetByIdAsync(sessionId, ct);
            if (session == null) return null;
            else
            {
                var model = _mapper.Map<Session, UpdateSessionViewModel>(session);
                return model;
            }
        }

        public async Task<IEnumerable<TrainerSelectViewModel>> GetTrainerForDropDownAsync(CancellationToken ct = default)
        {
            var result = await _unitOfWork.GetRepository<Trainer>().GetAllAsync(ct: ct);
            return _mapper.Map<IEnumerable<TrainerSelectViewModel>>(result);
        }

        public async Task<Result> UpdateSessionAsync(int sessionId, UpdateSessionViewModel model, CancellationToken ct = default)
        {
            var session = await _unitOfWork.SessionRepository.GetByIdAsync(sessionId);
            if (session == null) return Result.NotFound("Session not found");
            if (session.EndDate < DateTime.Now) return Result.Validation("Can't updated an ended session");
            else
            {
                _mapper.Map<Session>(model);
                session.UpdatedAt = DateTime.Now;
                _unitOfWork.SessionRepository.UpdateAsync(session);
                var result = await _unitOfWork.SaveChangesAsync(ct);
                return result > 0?Result.Ok():Result.Fail("Failed to update");
            }
        }
    }
}
