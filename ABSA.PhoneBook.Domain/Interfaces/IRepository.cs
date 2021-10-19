using System.Collections.Generic;
using System.Threading.Tasks;
namespace ABSA.PhoneBook.Domain.Interfaces {
    public interface IRepository<TEntity> where TEntity : IEntity {
        Task<TEntity> Create (TEntity entity);
        Task<TEntity> GetEntityById (int id);

        Task<IEnumerable<TEntity>> GetEntities ();
        Task<IEnumerable<TEntity>> GetEntities (string searchCriteria);
        Task<IEnumerable<TEntity>> GetEntities (int page, int pageSize, string searchCriteria);
        Task<bool> UpdateEntity(TEntity entity);
        Task<bool> DeleteEntity(TEntity entity);
    }
}