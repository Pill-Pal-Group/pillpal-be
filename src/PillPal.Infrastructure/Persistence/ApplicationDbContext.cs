using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PillPal.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext() : base()
    {
    }

    //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    //    : base(options)
    //{
    //}

    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerPackage> CustomerPackages { get; set; }
    public DbSet<PackageCategory> PackageCategories { get; set; }
    public DbSet<Pharmacist> Pharmacists { get; set; }
    public DbSet<Drug> Drugs { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<DrugInformation> DrugInformations { get; set; }
    public DbSet<DetailBrand> DetailBrands { get; set; }
    public DbSet<PharmacyStore> PharmacyStores { get; set; }
    public DbSet<Prescript> Prescriptions { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<ScheduleDetail> ScheduleDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //optionsBuilder.UseSqlServer(_config.GetConnectionString("PillPalDb"));
        }
        optionsBuilder.UseSqlServer("Server=.;Database=PillPalDb;uid=sa;pwd=12345;TrustServerCertificate=True");
    }

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

        builder.ApplyConfiguration(new CustomerConfig());
        builder.ApplyConfiguration(new PharmacistConfig());
        builder.ApplyConfiguration(new ScheduleConfig());
        builder.ApplyConfiguration(new ScheduleDetailConfig());
        builder.ApplyConfiguration(new CustomerPackageConfig());
        builder.ApplyConfiguration(new PrescriptConfig());
        builder.ApplyConfiguration(new DrugPrescriptConfig());
        builder.ApplyConfiguration(new PackageCategoryConfig());
        builder.ApplyConfiguration(new DrugConfig());
        builder.ApplyConfiguration(new IngredientConfig());
        builder.ApplyConfiguration(new DrugInformationConfig());
        builder.ApplyConfiguration(new DetailBrandConfig());
        builder.ApplyConfiguration(new PharmacyStoreConfig());
    }
}
