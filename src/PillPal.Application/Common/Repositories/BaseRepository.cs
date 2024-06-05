using PillPal.Application.Common.Interfaces.Data;

namespace PillPal.Application.Common.Repositories;

public class BaseRepository
{
    protected readonly IApplicationDbContext Context = null!;

    protected readonly IMapper Mapper = null!;

    protected readonly IServiceProvider ServiceProvider;

    protected BaseRepository(
        IApplicationDbContext context,
        IMapper mapper,
        IServiceProvider serviceProvider)
    {
        Context = context;
        Mapper = mapper;
        ServiceProvider = serviceProvider;
    }

    protected BaseRepository(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    protected IValidator<T> GetValidator<T>()
    {
        return ServiceProvider.GetRequiredService<IValidator<T>>();
    }
}
