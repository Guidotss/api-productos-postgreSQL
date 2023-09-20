using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet; 

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>(); 
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await dbSet.FindAsync(id); 
        }


        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includesProperties = "", bool isTracking = true)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }


            if(orderBy != null)
            {
                query = orderBy(query); 
            }

            if(includesProperties != null)
            {
                foreach(var includesProp in includesProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includesProp); 
                }
            }

            if(isTracking != null)
            {
                   query = query.AsNoTracking();    
            }


            return await query.ToListAsync(); 
        }
        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> filter = null,string includesProperties = "",bool isTracking = true)
        {
            IQueryable<T> query = dbSet; 

            if(filter != null)
            {
                query = query.Where(filter); 
            }

            if (includesProperties != null)
            {
                foreach(var includeProps in includesProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) 
                {
                    query = query.Include(includeProps);
                }
            }

            if(isTracking != null)
            {
                query = query.AsNoTracking(); 
            }
                
            return await query.FirstOrDefaultAsync();
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);  
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        } 

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity); 
        }
    }


}
