namespace PillPal.Application.Common.Interfaces.Data;

public interface IApplicationDbContext
{
    DbSet<ActiveIngredient> ActiveIngredients { get; }
    DbSet<Brand> Brands { get; }
    DbSet<Category> Categories { get; }
    DbSet<Customer> Customers { get; }
    DbSet<CustomerPackage> CustomerPackages { get; }
    DbSet<DosageForm> DosageForms { get; }
    DbSet<MedicationTake> MedicationTakes { get; }
    DbSet<Medicine> Medicines { get; }
    DbSet<MedicineInBrand> MedicineInBrands { get; }
    DbSet<Nation> Nations { get; }
    DbSet<PackageCategory> PackageCategories { get; }
    DbSet<Payment> Payments { get; }
    DbSet<PharmaceuticalCompany> PharmaceuticalCompanies { get; }
    DbSet<PharmacyStore> PharmacyStores { get; }
    DbSet<Prescript> Prescripts { get; }
    DbSet<PrescriptDetail> PrescriptDetails { get; }
    DbSet<Specification> Specifications { get; }
    DbSet<TermsOfService> TermsOfServices { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
