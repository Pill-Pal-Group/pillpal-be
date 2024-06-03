namespace PillPal.Infrastructure.Persistence.Configurations;

public class PrescriptDetailConfig : IEntityTypeConfiguration<PrescriptDetail>
{
    public void Configure(EntityTypeBuilder<PrescriptDetail> builder)
    {
        builder.HasKey(pd => pd.Id);
        builder.Property(pd => pd.Id).ValueGeneratedOnAdd();
    }
}
