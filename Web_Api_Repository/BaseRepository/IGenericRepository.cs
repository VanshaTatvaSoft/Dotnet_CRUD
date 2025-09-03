using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Web_Api_Repository.BaseEntities;

namespace Web_Api_Repository.BaseRepository;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, 
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
    (IEnumerable<T> records, int totalRecord) GetPagedRecords(
        int pageSize,
        int pageNumber,
        Expression<Func<T, bool>> filter,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy
    );
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task EditAsync(T entity);
    Task DeleteAsync(T entity);
    Task DeleteById(int id);
    Task AddRangeSync(IEnumerable<T> entities);
    Task EditRangeSync(IEnumerable<T> entities);
    Task DeleteRangeSync(IEnumerable<T> entities);
    Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicates,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null
    );
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    Task SoftDeleteAsync(T entity);
}
