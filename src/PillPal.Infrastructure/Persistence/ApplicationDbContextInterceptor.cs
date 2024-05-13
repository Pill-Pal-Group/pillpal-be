using Microsoft.EntityFrameworkCore.Diagnostics;
using PillPal.Core.Common;

namespace PillPal.Infrastructure.Persistence
{
    public class ApplicationDbContextInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var entries = eventData.Context?.ChangeTracker.Entries()
                .Where(e => e.Entity is BaseAuditableEntity &&
                        (e.State == EntityState.Added || e.State == EntityState.Modified));

            if (entries != null)
            {
                foreach (var entity in entries) 
                {
                    if (entity.State == EntityState.Added)
                    {
                        ((BaseAuditableEntity)entity.Entity).CreatedAt = DateTime.UtcNow;
                    }
                    else if (entity.State == EntityState.Modified)
                    {
                        ((BaseAuditableEntity)entity.Entity).UpdatedAt = DateTime.UtcNow;
                    }
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
