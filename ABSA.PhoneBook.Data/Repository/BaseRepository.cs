using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ABSA.PhoneBook.Data.Context;
using ABSA.PhoneBook.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ABSA.PhoneBook.Data.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Domain.Entities.BaseEntity
    {
        private readonly PhoneBookDbContext _context;
        private readonly DbSet<TEntity> _set;

        public BaseRepository(PhoneBookDbContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<TEntity> Create(TEntity entity)
        {
            return (await _set.AddAsync(entity)).Entity;
        }

        public async Task DeleteEntity(TEntity entity)
        {
            _set.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetEntities()
        {
            return await _set.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetEntities(Expression<Func<TEntity, bool>> predicate)
        {
            return await _set.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetEntities(int page, int pageSize, Expression<Func<TEntity, bool>> predicate)
        {
            return await _set.Where(predicate).Skip(page - 1).Take(pageSize).ToListAsync();
        }

        public async Task<TEntity> GetEntityById(int id)
        {
            return await _set.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetTotalCount()
        {
            return await _set.CountAsync();
        }

        public async Task UpdateEntity(TEntity entity)
        {
            _set.Update(entity);
        }
    }
}