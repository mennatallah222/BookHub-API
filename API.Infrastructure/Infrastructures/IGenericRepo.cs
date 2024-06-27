using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Infrastructure.Infrastructures
{
    public interface IGenericRepo<T> where T : class
    {
        
        Task<T> GetIDAsync(int id);


        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);


        Task DeleteRangeAsync(ICollection<T> entities);
        Task AddRangeAsync(ICollection<T> entities);
        Task UpadteRangeAsync(ICollection<T> entities);


        IQueryable<T> GetTableAsTrasking();
        IQueryable<T> GetTableNoTrasking();


        void Commit();
        void Rollback();

        Task SaveChangesAsync();
        IDbContextTransaction BeginTransaction();
    }
}
