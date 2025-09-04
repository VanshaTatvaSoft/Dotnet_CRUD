using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Web_Api_Repository.BaseEntities;
using Web_Api_Repository.DTO;
using Web_Api_Repository.Models;

namespace Web_Api_Repository.BaseRepository;

public class GenericRepository<T>(ProductManagementContext context) : IGenericRepository<T> where T : BaseEntity
{
    private readonly ProductManagementContext _context = context;
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<List<TResult>> GetAllAsync<TResult>(QueryOptions<T, TResult> options)
    {
        IQueryable<T> query = _dbSet;
        if (options.Includes != null) 
        {
            foreach (var include in options.Includes)
            {
                query = query.Include(include);
            }
        }
        if (options.Predicate != null) query = query.Where(options.Predicate);
        if (options.OrderBy != null) query = query.OrderBy(options.OrderBy);
        if (options.Selector != null) return await query.Select(options.Selector).ToListAsync(options.CancellationToken);
        return await query.Cast<TResult>().ToListAsync(options.CancellationToken);
    }

    public async Task<int> CountAllAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _dbSet;
        if (predicate != null) query = query.Where(predicate);
        return await query.CountAsync(cancellationToken);
    }

    public (IEnumerable<T> records, int totalRecord) GetPagedRecords(
        int pageSize,
        int pageNumber,
        Expression<Func<T, bool>> filter,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy
    )
    {
        if (orderBy == null) throw new ArgumentNullException(nameof(orderBy), "Ordering function cannot be null.");

        IQueryable<T> query = _dbSet;

        if (filter != null) query = query.Where(filter);

        return (orderBy(query).Skip((pageNumber - 1) * pageSize).Take(pageSize), query.Count());
    }

    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted, cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task EditAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteById(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id) ?? throw new KeyNotFoundException($"{typeof(T).Name} with id {id} not found.");
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddRangeSync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        if (entities == null || !entities.Any()) throw new ArgumentNullException(nameof(entities), "Entities cannot be null or empty.");
        await _dbSet.AddRangeAsync(entities, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task EditRangeSync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        if (entities == null || !entities.Any()) throw new ArgumentNullException(nameof(entities), "Entities cannot be null or empty.");
        _dbSet.UpdateRange(entities);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRangeSync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        if (entities == null || !entities.Any()) throw new ArgumentNullException(nameof(entities), "Entities cannot be null or empty.");
        _dbSet.RemoveRange(entities);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicates,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<T> query = _dbSet;
        if (includes != null) query = includes(query);
        return await query.Where(predicates).ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }

    public async Task SoftDeleteAsync(T entity, CancellationToken cancellationToken)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
        var property = typeof(T).GetProperty("IsDeleted");
        if (property != null)
        {
            property.SetValue(entity, true);
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            throw new InvalidOperationException("Entity does not support soft delete.");
        }
    }
}
