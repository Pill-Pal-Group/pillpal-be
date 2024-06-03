namespace PillPal.Infrastructure.Persistence.Configurations;

public class NationConfig : IEntityTypeConfiguration<Nation>
{
    public void Configure(EntityTypeBuilder<Nation> builder)
    {
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id).ValueGeneratedOnAdd();
    }
}
