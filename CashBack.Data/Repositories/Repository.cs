using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CashBack.Data.Context;
using CashBack.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CashBack.Data.Repositories
{
    /// <summary>
    /// Responsável por estabelecer os contratos básicos que toda interface de repositório deve implementar
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly CashBackContext Context;

        protected Repository(CashBackContext context)
        {
            Context = context;
        }

        public ValueTask<TEntity> GetAsync(int id)
        {
            return Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }
        
        public void DeleteAsync(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public async Task UpdateASync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }
    }
}
