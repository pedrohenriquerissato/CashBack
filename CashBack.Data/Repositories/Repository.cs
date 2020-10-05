using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CashBack.Data.Context;
using CashBack.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CashBack.Data.Repositories
{
    public class Repository<Entity> : IRepository<Entity> where Entity : class
    {
        protected readonly CashBackContext Context;

        public Repository(CashBackContext context)
        {
            Context = context;
        }

        public ValueTask<Entity> GetAsync(int id)
        {
            return Context.Set<Entity>().FindAsync(id);
        }

        public async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await Context.Set<Entity>().ToListAsync();
        }

        public async Task<Entity> SingleOrDefaultAsync(Expression<Func<Entity, bool>> predicate)
        {
            return await Context.Set<Entity>().SingleOrDefaultAsync(predicate);
        }

        public async Task InsertAsync(Entity entity)
        {
            await Context.Set<Entity>().AddAsync(entity);
        }
        
        public void DeleteAsync(Entity entity)
        {
            Context.Set<Entity>().Remove(entity);
        }

        public async Task UpdateASync(Entity entity)
        {
            await Context.Set<Entity>().AddAsync(entity);
        }
    }
}
