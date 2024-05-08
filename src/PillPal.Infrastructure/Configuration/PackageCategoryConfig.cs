namespace PillPal.Infrastructure.Configuration;

public class PackageCategoryConfig : IEntityTypeConfiguration<PackageCategory>
{
    public void Configure(EntityTypeBuilder<PackageCategory> entity)
    {
        entity.ToTable("PackageCategory");
        entity.Property(e => e.Id).HasColumnName("PackageCategoryId");
    }
}
