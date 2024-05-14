using Microsoft.EntityFrameworkCore.Diagnostics;

namespace PillPal.Infrastructure.Persistence;

public class UniqueCodeInterceptor : SaveChangesInterceptor
{
    #region unique code gen
    private static string GenerateUniqueCode(string prefix, DbContextEventData eventData)
    {
        string currentSeconds = DateTime.Now.ToString("ss");

        int sequenceCount = eventData!.Context!.Database
            .ExecuteSql($"SELECT NEXT VALUE FOR {prefix}");

        string paddedSequenceString = sequenceCount
            .ToString().PadLeft(6, '0');

        Random random = new Random();
        string randomNumber = random.Next(1, 100000).ToString();
        string paddedRandomNumberString = randomNumber.PadLeft(5, '0');

        return $"{prefix}{currentSeconds}-{paddedSequenceString}-{paddedRandomNumberString}";
    }
    #endregion

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var entries = eventData.Context?.ChangeTracker.Entries()
            .Where(entry => entry.State == EntityState.Added);

        if (entries != null)
        {
            foreach (var entry in entries)
            {
                if (entry.Entity is Ingredient)
                {
                    entry.CurrentValues["IngredientCode"] = GenerateUniqueCode("IGR", eventData);
                }


            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
