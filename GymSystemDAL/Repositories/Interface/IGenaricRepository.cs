using GymSystem.Models;
using GymSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
<<<<<<< HEAD
<<<<<<< HEAD
using System.Linq.Expressions;
=======
>>>>>>> dev
=======
using System.Linq.Expressions;
>>>>>>> backup-before-merge
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Repositories.Interface
{
    public interface IGenaricRepository<TEntity> where TEntity : BaseEntity , new()
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = false, CancellationToken ct = default);
        Task<TEntity> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> AddAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
<<<<<<< HEAD
<<<<<<< HEAD
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);

        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,bool tracking = false , CancellationToken ct=default);
=======
>>>>>>> dev
=======

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);

        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,bool tracking = false , CancellationToken ct=default);

>>>>>>> backup-before-merge
    }
}
