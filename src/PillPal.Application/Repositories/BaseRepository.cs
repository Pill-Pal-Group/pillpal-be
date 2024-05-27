using PillPal.Application.Common.Interfaces.Data;

namespace PillPal.Application.Repositories;

public class BaseRepository
{
    protected readonly IApplicationDbContext _context;

    protected readonly IMapper _mapper;

    protected readonly IServiceProvider _serviceProvider;

    protected BaseRepository(
        IApplicationDbContext context,
        IMapper mapper,
        IServiceProvider serviceProvider)
    {
        _context = context;
        _mapper = mapper;
        _serviceProvider = serviceProvider;
    }

    protected IValidator<T> GetValidator<T>()
    {
        return _serviceProvider.GetRequiredService<IValidator<T>>();
    }
}
