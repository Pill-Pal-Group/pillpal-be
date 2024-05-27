namespace PillPal.Infrastructure.Persistence.Configurations;

public class PharmaceuticalCompanyConfig : IEntityTypeConfiguration<PharmaceuticalCompany>
{
    public void Configure(EntityTypeBuilder<PharmaceuticalCompany> builder)
    {
        builder.HasKey(pc => pc.Id);
        builder.Property(pc => pc.Id).ValueGeneratedOnAdd();
    }
}
