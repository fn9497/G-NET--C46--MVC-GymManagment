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
    public class GenaricRepository<TEntity> : IGenaricRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly GymDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenaricRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = false, CancellationToken ct = default)
        {
            IQueryable<TEntity> query = tracking ? _dbSet : _dbSet.AsNoTracking();
            return await query.ToListAsync(ct);
        }

        public async Task<TEntity> GetByIdAsync(int id, CancellationToken ct = default)
        {
           return await _dbSet.FindAsync(new object[] { id }, ct);
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
