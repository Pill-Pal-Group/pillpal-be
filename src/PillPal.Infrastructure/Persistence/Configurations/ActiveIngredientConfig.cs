namespace PillPal.Infrastructure.Persistence.Configurations;

public class ActiveIngredientConfig : IEntityTypeConfiguration<ActiveIngredient>
{
    public void Configure(EntityTypeBuilder<ActiveIngredient> builder)
    {
        builder.HasKey(ai => ai.Id);
        builder.Property(ai => ai.Id).ValueGeneratedOnAdd();
    }
}