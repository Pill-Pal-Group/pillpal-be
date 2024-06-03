using PillPal.Application.Common.Interfaces.Data;

namespace PillPal.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options), IApplicationDbContext
{
    public DbSet<ActiveIngredient> ActiveIngredients { get; set; }

    public DbSet<Brand> Brands { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<CustomerPackage> CustomerPackages { get; set; }

    public DbSet<DosageForm> DosageForms { get; set; }

    public DbSet<MedicationTake> MedicationTakes { get; set; }

    public DbSet<Medicine> Medicines { get; set; }

    public DbSet<MedicineInBrand> MedicineInBrands { get; set; }

    public DbSet<Nation> Nations { get; set; }

    public DbSet<PackageCategory> PackageCategories { get; set; }

    public DbSet<Payment> Payments { get; set; }

    public DbSet<PharmaceuticalCompany> PharmaceuticalCompanies { get; set; }

    public DbSet<PharmacyStore> PharmacyStores { get; set; }

    public DbSet<Prescript> Prescripts { get; set; }

    public DbSet<PrescriptDetail> PrescriptDetails { get; set; }

    public DbSet<Specification> Specifications { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region identity
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("Account");
            entity.Property(e => e.Id).HasColumnName("AccountId");
        });

        builder.Entity<IdentityRole<Guid>>(entity =>
        {
            entity.ToTable("Role");
            entity.Property(e => e.Id).HasColumnName("RoleId");
        });

        builder.Entity<IdentityUserRole<Guid>>(entity =>
        {
            entity.ToTable("AccountRole");
            entity.Property(e => e.UserId).HasColumnName("AccountId");
            entity.Property(e => e.RoleId).HasColumnName("RoleId");
        });

        builder.Entity<IdentityUserLogin<Guid>>(entity =>
        {
            entity.ToTable("AccountLogin");
            entity.Property(e => e.UserId).HasColumnName("AccountId");
        });

        builder.Entity<IdentityUserToken<Guid>>(entity =>
        {
            entity.ToTable("AccountToken");
            entity.Property(e => e.UserId).HasColumnName("AccountId");
        });

        builder.Ignore<IdentityUserClaim<Guid>>();
        builder.Ignore<IdentityRoleClaim<Guid>>();
        #endregion

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
