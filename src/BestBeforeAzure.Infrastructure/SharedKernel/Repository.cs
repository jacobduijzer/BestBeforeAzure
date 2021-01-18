using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BestBeforeAzure.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace BestBeforeAzure.Infrastructure.SharedKernel
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        private readonly BestBeforeDbContext _bestBeforeDbContext;

        public Repository(BestBeforeDbContext bestBeforeDbContext)
        {
            _bestBeforeDbContext = bestBeforeDbContext;
        }

        public async Task Add(TEntity entity)
        {
            await _bestBeforeDbContext
                .Set<TEntity>()
                .AddAsync(entity)
                .ConfigureAwait(false);
            await _bestBeforeDbContext
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> expression) =>
            await _bestBeforeDbContext
                .Set<TEntity>()
                .Where(expression)
                .ToListAsync()
                .ConfigureAwait(false);

        public Task<TEntity> FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task Update(TEntity entity)
        {
            _bestBeforeDbContext
                .Update(entity);

            await _bestBeforeDbContext
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }
    }
}
