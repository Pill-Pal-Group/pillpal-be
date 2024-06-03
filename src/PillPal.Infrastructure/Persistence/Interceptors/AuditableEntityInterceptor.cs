using PillPal.Application.Common.Interfaces.Data;

namespace PillPal.Infrastructure.Persistence.Interceptors;

public class AuditableEntityInterceptor(IUser user)
    : SaveChangesInterceptor
{
    private readonly IUser _user = user;

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        UpdateEntites(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateEntites(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntites(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if (entry.State is EntityState.Added or EntityState.Modified)
            {
                var utcNow = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = _user.Id;
                    entry.Entity.CreatedAt = utcNow;
                }
                entry.Entity.UpdatedBy = _user.Id;
                entry.Entity.UpdatedAt = utcNow;
            }
        }
    }
}
