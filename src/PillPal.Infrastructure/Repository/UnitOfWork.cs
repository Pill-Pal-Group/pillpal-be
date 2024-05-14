using PillPal.Infrastructure.Persistence;

namespace PillPal.Infrastructure.Repository;

public class UnitOfWork : IDisposable
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    private GenericRepository<Drug>? _drug;
    public GenericRepository<Drug> Drug
    {
        get
        {
            _drug ??= new GenericRepository<Drug>(_context);
            return _drug;
        }
    }

    private GenericRepository<DrugInformation>? _drugInformation;
    public GenericRepository<DrugInformation> DrugInformation
    {
        get
        {
            _drugInformation ??= new GenericRepository<DrugInformation>(_context);
            return _drugInformation;
        }
    }

    private GenericRepository<Ingredient>? _ingredient;
    public GenericRepository<Ingredient> Ingredient
    {
        get
        {
            _ingredient ??= new GenericRepository<Ingredient>(_context);
            return _ingredient;
        }
    }

    private GenericRepository<Customer>? _customer;
    public GenericRepository<Customer> Customer
    {
        get
        {
            _customer ??= new GenericRepository<Customer>(_context);
            return _customer;
        }
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync()) > 0;
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
