namespace PillPal.Infrastructure.Persistence.Configurations;

public class SpecificationConfig : IEntityTypeConfiguration<Specification>
{
    public void Configure(EntityTypeBuilder<Specification> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedOnAdd();
    }
}
