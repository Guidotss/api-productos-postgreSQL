using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(string id);

        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            string includeProperties = "",
            bool isTracking = true
        );

        Task<T> GetFirstAsync(
            Expression<Func<T, bool>> filter = null,
            string includesProperties = "",
            bool isTracking = true
        );

        Task AddAsync();

        void Remove(string id); 


        void RemoveRange(IEnumerable<T> entity); 
        
    }
}
