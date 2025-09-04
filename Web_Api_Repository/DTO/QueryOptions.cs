using System.Linq.Expressions;

namespace Web_Api_Repository.DTO;

public record QueryOptions<T, TResult>
(
    Expression<Func<T, bool>> Predicate = null,
    List<Expression<Func<T, object>>> Includes = null,
    Expression<Func<T, object>> OrderBy = null,
    Expression<Func<T, TResult>> Selector = null,
    CancellationToken CancellationToken = default
)
{
    public List<Expression<Func<T, object>>> Includes { get; init; } = Includes ?? [];
}
