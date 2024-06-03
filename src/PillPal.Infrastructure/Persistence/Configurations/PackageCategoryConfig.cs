namespace PillPal.Infrastructure.Persistence.Configurations;

public class PackageCategoryConfig : IEntityTypeConfiguration<PackageCategory>
{
    public void Configure(EntityTypeBuilder<PackageCategory> builder)
    {
        builder.HasKey(pc => pc.Id);
        builder.Property(pc => pc.Id).ValueGeneratedOnAdd();
    }
}
