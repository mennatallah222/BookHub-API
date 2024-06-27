using API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Infrastructure.Infrastructures
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly ApplicationDBContext _dbContext;

        #region CTOR
        public GenericRepo(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        #endregion


        #region ACTIONS
        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task AddRangeAsync(ICollection<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            //return entities;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
             _dbContext.Database.CommitTransaction();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteRangeAsync(ICollection<T> entities)
        {
            foreach(var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<T> GetIDAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetTableAsTrasking()
        {
            return  _dbContext.Set<T>().AsQueryable();

        }

        public IQueryable<T> GetTableNoTrasking()
        {
            return  _dbContext.Set<T>().AsNoTracking().AsQueryable();

        }

        public void Rollback()
        {
            _dbContext.Database.RollbackTransaction();

        }

        public async Task SaveChangesAsync()
        {
           await _dbContext.SaveChangesAsync();
        }

        public virtual async Task UpadteRangeAsync(ICollection<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion


        
    }
}
