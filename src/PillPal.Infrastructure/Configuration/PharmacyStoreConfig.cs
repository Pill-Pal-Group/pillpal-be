namespace PillPal.Infrastructure.Configuration;

public class PharmacyStoreConfig : IEntityTypeConfiguration<PharmacyStore>
{
    public void Configure(EntityTypeBuilder<PharmacyStore> entity)
    {
        entity.ToTable("PharmacyStore");
        entity.Property(e => e.Id).HasColumnName("PharmacyStoreId");

        entity.HasOne(d => d.DetailBrand)
            .WithMany(p => p.PharmacyStores)
            .HasForeignKey(d => d.BrandId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
