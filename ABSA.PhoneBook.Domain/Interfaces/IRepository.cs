using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ABSA.PhoneBook.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity {
        Task<TEntity> Create (TEntity entity);
        Task<TEntity> GetEntityById (int id);
        Task<int> GetTotalCount();

        Task<int> GetTotalCount(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetEntities ();
        Task<IEnumerable<TEntity>> GetEntities (Expression<Func<TEntity,bool>> predicate);
        Task<IEnumerable<TEntity>> GetEntities (int page, int pageSize, Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetEntities(int page, int pageSize, params Expression<Func<TEntity, bool>> [] predicates);
        Task UpdateEntity(TEntity entity);
        Task DeleteEntity(TEntity entity);
        public IUnitOfWork UnitOfWork { get;  }
    }
}