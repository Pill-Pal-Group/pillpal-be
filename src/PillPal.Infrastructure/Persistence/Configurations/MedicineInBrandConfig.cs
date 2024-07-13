namespace PillPal.Infrastructure.Persistence.Configurations;

public class MedicineInBrandConfig : IEntityTypeConfiguration<MedicineInBrand>
{
    public void Configure(EntityTypeBuilder<MedicineInBrand> builder)
    {
        builder.HasKey(entity => new { entity.BrandId, entity.MedicineId });
    }
}
