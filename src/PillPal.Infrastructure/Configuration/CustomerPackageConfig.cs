namespace PillPal.Infrastructure.Configuration;

public class CustomerPackageConfig : IEntityTypeConfiguration<CustomerPackage>
{
    public void Configure(EntityTypeBuilder<CustomerPackage> entity)
    {
        entity.ToTable("CustomerPackage");
        entity.Property(e => e.Id).HasColumnName("CustomerPackageId");
        entity.Property(e => e.CustomerId).HasColumnName("CustomerId");

        entity.HasOne(d => d.PackageCategory)
            .WithMany(p => p.CustomerPackages)
            .HasForeignKey(d => d.PackageCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
