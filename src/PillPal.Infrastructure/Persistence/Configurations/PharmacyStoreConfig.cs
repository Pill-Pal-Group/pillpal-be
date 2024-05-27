namespace PillPal.Infrastructure.Persistence.Configurations;

public class PharmacyStoreConfig : IEntityTypeConfiguration<PharmacyStore>
{
    public void Configure(EntityTypeBuilder<PharmacyStore> builder)
    {
        builder.HasKey(ps => ps.Id);
        builder.Property(ps => ps.Id).ValueGeneratedOnAdd();
    }
}
