namespace PillPal.Infrastructure.Configuration;

public class DrugPrescriptConfig : IEntityTypeConfiguration<DrugPrescript>
{
    public void Configure(EntityTypeBuilder<DrugPrescript> entity)
    {
        entity.ToTable("DrugPrescript");
        entity.Property(e => e.PrescriptId).HasColumnName("PrescriptId");
        entity.Property(e => e.DrugId).HasColumnName("DrugId");

        entity.HasKey(e => new { e.PrescriptId, e.DrugId });

        entity.HasOne(d => d.Drug)
            .WithMany(p => p.DrugPrescripts)
            .HasForeignKey(d => d.DrugId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
