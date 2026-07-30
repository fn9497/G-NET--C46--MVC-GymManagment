using GymSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Repositories.Interface
{
    public interface ISessionRepository : IGenaricRepository<Session>
    {
        Task<IEnumerable<Session>> GetAllSessionWithTrainersAndCategories(CancellationToken ct);
        Task<Session> GetSessionWithTrainerAndCategory(int sessionId , CancellationToken ct=default);
        Task<int> GetCountOfBookSlots(int sessionId, CancellationToken ct = default);

    }
}
