namespace PillPal.Infrastructure.Configuration;

public class IngredientConfig : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> entity)
    {
        entity.ToTable("Ingredient");
        entity.Property(e => e.Id).HasColumnName("IngredientId");
    }
}
