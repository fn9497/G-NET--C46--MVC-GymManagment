using GymSystem.DbContexts;
using GymSystemDAL.Models;
using GymSystemDAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Repositories.Classes
{
    public class SessionRepository : GenaricRepository<Session>, ISessionRepository
    {
        private readonly GymDbContext _dbContext;

        public SessionRepository(GymDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Session>> GetAllSessionWithTrainersAndCategories(CancellationToken ct)
        {
            var query = _dbContext.Sessions.AsNoTracking().Include(t => t.Trainer).Include(c => c.Category);
            return await query.ToListAsync();
        }

        public async Task<int> GetCountOfBookSlots(int sessionId, CancellationToken ct = default)
        {
            return await _dbContext.Bookings.AsNoTracking().CountAsync(b => b.SessionId == sessionId);
        }

        public async Task<Session> GetSessionWithTrainerAndCategory(int sessionId, CancellationToken ct = default)
        {
            return await _dbContext.Sessions.AsNoTracking().Include(t => t.Trainer).Include(c => c.Category).FirstOrDefaultAsync(s=>s.Id == sessionId);
        }
    }
}
