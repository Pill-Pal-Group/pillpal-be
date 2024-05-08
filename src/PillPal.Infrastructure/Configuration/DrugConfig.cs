namespace PillPal.Infrastructure.Configuration;

public class DrugConfig : IEntityTypeConfiguration<Drug>
{
    public void Configure(EntityTypeBuilder<Drug> entity)
    {
        entity.ToTable("Drug");
        entity.Property(e => e.Id).HasColumnName("DrugId");

        entity.HasMany(d => d.DrugInformations)
            .WithOne(p => p.Drug)
            .HasForeignKey(d => d.DrugId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(d => d.Ingredients)
            .WithMany(p => p.Drugs)
            .UsingEntity<Dictionary<string, object>>(
                "DrugIngredient",
                r => r.HasOne<Ingredient>().WithMany().HasForeignKey("IngredientId"),
                l => l.HasOne<Drug>().WithMany().HasForeignKey("DrugId"),
                je =>
                {
                    je.HasKey("DrugId", "IngredientId");
                    je.ToTable("DrugIngredient");
                });

        entity.HasMany(d => d.PharmacyStores)
            .WithMany(p => p.Drugs)
            .UsingEntity<Dictionary<string, object>>(
                "DrugStore",
                r => r.HasOne<PharmacyStore>().WithMany().HasForeignKey("PharmacyStoreId"),
                l => l.HasOne<Drug>().WithMany().HasForeignKey("DrugId"),
                je =>
                {
                    je.HasKey("DrugId", "PharmacyStoreId");
                    je.ToTable("DrugStore");
                });
    }
}
