using System.Linq.Expressions;

namespace Web_Api_Repository.DTO;

public class QueryOptions<T, TResult>
{
    public Expression<Func<T, bool>> Predicate { get; set; } = null;
     public List<Expression<Func<T, object>>> Includes { get; set; } = [];
    public Expression<Func<T, object>> OrderBy { get; set; } = null;
    public Expression<Func<T, TResult>> Selector { get; set; } = null;
    public CancellationToken CancellationToken { get; set; } = default;
}
