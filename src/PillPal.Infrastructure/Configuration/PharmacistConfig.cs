namespace PillPal.Infrastructure.Configuration;

public class PharmacistConfig : IEntityTypeConfiguration<Pharmacist>
{
    public void Configure(EntityTypeBuilder<Pharmacist> entity)
    {
        entity.ToTable("Pharmacist");
        entity.Property(e => e.Id).HasColumnName("PharmacistId");
        entity.Property(e => e.IdentityUserId).HasColumnName("AccountId");

        entity.HasIndex(e => e.PharmacistCode).IsUnique();
        entity.HasIndex(e => e.IdentityUserId).IsUnique();

        entity.HasOne(d => d.IdentityUser)
            .WithOne(p => p.Pharmacist)
            .HasForeignKey<Pharmacist>(d => d.IdentityUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
