namespace PillPal.Infrastructure.Persistence.Configurations;

public class CustomerPackageConfig : IEntityTypeConfiguration<CustomerPackage>
{
    public void Configure(EntityTypeBuilder<CustomerPackage> builder)
    {
        builder.HasKey(cp => cp.Id);
        builder.Property(cp => cp.Id).ValueGeneratedOnAdd();
    }
}
