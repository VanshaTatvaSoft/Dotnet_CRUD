using Web_Api_Repository.BaseRepository;
using Web_Api_Repository.Models;

namespace Web_Api_Repository.UnitOfWork;

public interface IUnitOfWork: IDisposable
{
    IGenericRepository<Product> Products { get; }
    IGenericRepository<Category> Categories { get; }
    Task<int> SaveAsync();
}
