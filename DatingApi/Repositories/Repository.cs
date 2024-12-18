
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DatingApi.Repositories
{
    public class Repository<T>(DbContext context) : IRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet = context.Set<T>();
        public async Task<T> Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) throw new Exception("entity not found");
            _dbSet.Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<T> Get(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> Update(T entity)
        {
            _dbSet.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task<List<T>> GetAllIncludesAsync(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int skip = 0, int take = int.MaxValue,
        params Expression<Func<T, object>>[]? includeProperties)
        {
            IQueryable<T?> query = _dbSet.AsNoTracking();
            query = filter != null ? query.Where(filter!) : query;
            if (includeProperties != null)
                query = includeProperties.Aggregate(query, (current, include) => current.Include(include));
            query = query.Skip(skip).Take(take);
            return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
        }

        public async Task<T> GetIncludesAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[]? includeProperties)
        {
            IQueryable<T?> query = _dbSet.AsNoTracking();
            query = filter != null ? query.Where(filter!) : query;
            if (includeProperties != null)
                query = includeProperties.Aggregate(query, (current, include) => current.Include(include));
            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
