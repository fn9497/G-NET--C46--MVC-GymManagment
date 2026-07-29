using GymSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Repositories.Interface
{
    public interface IUnitOfWork
    {
        IGenaricRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new();

        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
