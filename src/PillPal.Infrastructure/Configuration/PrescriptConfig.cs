namespace PillPal.Infrastructure.Configuration;

public class PrescriptConfig : IEntityTypeConfiguration<Prescript>
{
    public void Configure(EntityTypeBuilder<Prescript> entity)
    {
        entity.ToTable("Prescript");
        entity.Property(e => e.Id).HasColumnName("PrescriptId");
        entity.Property(e => e.CustomerId).HasColumnName("CustomerId");

        entity.HasMany(d => d.DrugPrescripts)
            .WithOne(p => p.Prescript)
            .HasForeignKey(d => d.PrescriptId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
