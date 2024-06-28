namespace PillPal.Infrastructure.Persistence.Interceptors;

public class UniqueCodeInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        AddUniqueCode(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        AddUniqueCode(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void AddUniqueCode(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added)
            {
                switch (entry.Entity)
                {
                    case ActiveIngredient ingredient:
                        ingredient.IngredientCode = GenerateUniqueCodeUtility(EntityCode.IngredientCodePrefix);
                        break;
                    case Brand brand:
                        brand.BrandCode = GenerateUniqueCodeUtility(EntityCode.BrandCodePrefix);
                        break;
                    case Category category:
                        category.CategoryCode = GenerateUniqueCodeUtility(EntityCode.CategoryCodePrefix);
                        break;
                    case Customer customer:
                        customer.CustomerCode = GenerateUniqueCodeUtility(EntityCode.CustomerCodePrefix);
                        break;
                    case Medicine medicine:
                        medicine.MedicineCode = GenerateUniqueCodeUtility(EntityCode.MedicineCodePrefix);
                        break;
                    case PharmaceuticalCompany pharmaceuticalCompany:
                        pharmaceuticalCompany.CompanyCode = GenerateUniqueCodeUtility(EntityCode.PharmaceuticalCompanyCodePrefix);
                        break;
                    case TermsOfService termsOfService:
                        termsOfService.TosCode = GenerateUniqueCodeUtility(EntityCode.TermsOfServiceCodePrefix);
                        break;
                }
            }
        }
    }

    #region unique code gen
    public string GenerateUniqueCodeUtility(string prefix)
    {
        string currentSeconds = DateTime.Now.ToString("ss");
        string currentMinutes = DateTime.Now.ToString("mm");

        Random random = new();
        string randomNumber = random.Next(1, 1000000).ToString();
        string paddedRandomNumberString = randomNumber.PadLeft(6, '0');

        return $"{prefix}{currentMinutes}{currentSeconds}-{paddedRandomNumberString}";
    }
    #endregion
}
