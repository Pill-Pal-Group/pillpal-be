namespace PillPal.Infrastructure.Configuration;

public class DetailBrandConfig : IEntityTypeConfiguration<DetailBrand>
{
    public void Configure(EntityTypeBuilder<DetailBrand> entity)
    {
        entity.ToTable("DetailBrand");
        entity.Property(e => e.Id).HasColumnName("BrandId");
    }
}
