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

    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbContext _dbContext;
        private Dictionary<string, object> _repositories = [];

        public UnitOfWork(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenaricRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var typeName = typeof(TEntity).Name;
            //check if exist
            //exist --use 
            if (_repositories.TryGetValue(typeName, out object? value))
                return (IGenaricRepository<TEntity>)value;
            //does not exist --add
            else
            { 
                var repository = new GenaricRepository<TEntity>(_dbContext);
                _repositories.Add(typeName, repository);
                return repository;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)=> await _dbContext.SaveChangesAsync(ct);
    }
}
