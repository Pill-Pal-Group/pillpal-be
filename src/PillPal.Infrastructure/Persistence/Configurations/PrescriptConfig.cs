namespace PillPal.Infrastructure.Persistence.Configurations;

public class PrescriptConfig : IEntityTypeConfiguration<Prescript>
{
    public void Configure(EntityTypeBuilder<Prescript> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
    }
}
