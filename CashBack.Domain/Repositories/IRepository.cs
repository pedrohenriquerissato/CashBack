using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CashBack.Domain.Repositories
{
    public interface IRepository<Entity> where Entity : class
    {
        ValueTask<Entity> GetAsync(int id);
        Task<IEnumerable<Entity>> GetAllAsync();
        Task<Entity> SingleOrDefaultAsync(Expression<Func<Entity, bool>> predicate);
        Task InsertAsync(Entity entity);
        Task UpdateASync(Entity entity);
        void DeleteAsync(Entity entity);
    }
}
