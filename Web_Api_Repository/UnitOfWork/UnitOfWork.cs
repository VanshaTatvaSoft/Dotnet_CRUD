using Web_Api_Repository.BaseRepository;
using Web_Api_Repository.Models;

namespace Web_Api_Repository.UnitOfWork;

public class UnitOfWork: IUnitOfWork
{
    private readonly ProductManagementContext _context;
    public IGenericRepository<Product> Products { get; }
    public IGenericRepository<Category> Categories { get; }
    public UnitOfWork(ProductManagementContext context){
        _context = context;
        Products = new GenericRepository<Product>(_context);
        Categories = new GenericRepository<Category>(_context);
    }

    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
