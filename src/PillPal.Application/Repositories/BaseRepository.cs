using PillPal.Application.Common.Interfaces.Data;

namespace PillPal.Application.Repositories;

public class BaseRepository
{
    protected readonly IApplicationDbContext Context;

    protected readonly IMapper Mapper;

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

    protected IValidator<T> GetValidator<T>()
    {
        return ServiceProvider.GetRequiredService<IValidator<T>>();
    }
}
