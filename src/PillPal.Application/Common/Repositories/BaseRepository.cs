using PillPal.Application.Common.Interfaces.Data;
using PillPal.Core.Common;

namespace PillPal.Application.Common.Repositories;

public class BaseRepository
{
    protected readonly IApplicationDbContext Context;
    protected readonly IServiceProvider ServiceProvider;
    protected readonly IMapper Mapper = null!;

    protected BaseRepository(
        IApplicationDbContext context,
        IMapper mapper,
        IServiceProvider serviceProvider)
    {
        Context = context;
        Mapper = mapper;
        ServiceProvider = serviceProvider;
    }

    protected BaseRepository(
        IApplicationDbContext context,
        IServiceProvider serviceProvider)
    {
        Context = context;
        ServiceProvider = serviceProvider;
    }

    protected async Task ValidateAsync<T>(T dto)
    {
        var validator = ServiceProvider.GetRequiredService<IValidator<T>>();

        var validationResult = await validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    }

    protected static async Task<List<T>> GetEntitiesByIdsAsync<T>(IEnumerable<Guid> ids, DbSet<T> dbSet)
        where T : BaseEntity
    {
        return await dbSet
            .Where(e => ids.Contains(e.Id))
            .ToListAsync();
    }
}
