using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BestBeforeAzure.Domain.SharedKernel
{
    public interface IRepository<TEntity> where TEntity : IAggregateRoot
    {
        Task<TEntity> FindById(Guid id);
        //TEntity FindOne(ISpecification<TEntity> spec);
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> expression);
        Task Add(TEntity entity);
        void Remove(TEntity entity);
        Task Update(TEntity entity);
    }
}
