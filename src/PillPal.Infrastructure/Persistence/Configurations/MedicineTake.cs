namespace PillPal.Infrastructure.Persistence.Configurations;

public class MedicineTake : IEntityTypeConfiguration<MedicationTake>
{
    public void Configure(EntityTypeBuilder<MedicationTake> builder)
    {
        builder.HasKey(mt => mt.Id);
        builder.Property(mt => mt.Id).ValueGeneratedOnAdd();
    }
}
