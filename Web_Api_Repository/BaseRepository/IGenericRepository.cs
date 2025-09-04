using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Web_Api_Repository.BaseEntities;
using Web_Api_Repository.DTO;

namespace Web_Api_Repository.BaseRepository;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<List<TResult>> GetAllAsync<TResult>(QueryOptions<T, TResult> options);

    Task<int> CountAllAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default);
    
    (IEnumerable<T> records, int totalRecord) GetPagedRecords(
        int pageSize,
        int pageNumber,
        Expression<Func<T, bool>> filter,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy
    );
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task EditAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteById(int id, CancellationToken cancellationToken = default);
    Task AddRangeSync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task EditRangeSync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task DeleteRangeSync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicates,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null,
        CancellationToken cancellationToken = default
    );
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task SoftDeleteAsync(T entity, CancellationToken cancellationToken);
}
