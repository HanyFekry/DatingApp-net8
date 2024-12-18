using System.Linq.Expressions;

namespace DatingApi.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<bool> Delete(int id);
        Task<List<T>> GetAllIncludesAsync(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int skip = 0, int take = int.MaxValue,
        params Expression<Func<T, object>>[]? includeProperties);
        Task<T> GetIncludesAsync(Expression<Func<T, bool>>? filter = null,
        params Expression<Func<T, object>>[]? includeProperties);
        Task<int> SaveChangesAsync();
    }
}
