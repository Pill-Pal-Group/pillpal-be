namespace PillPal.Infrastructure.Persistence.Configurations;

public class DosageFormConfig : IEntityTypeConfiguration<DosageForm>
{
    public void Configure(EntityTypeBuilder<DosageForm> builder)
    {
        builder.HasKey(df => df.Id);
        builder.Property(df => df.Id).ValueGeneratedOnAdd();
    }
}
