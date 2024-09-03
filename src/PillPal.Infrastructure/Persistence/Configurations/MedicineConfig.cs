namespace PillPal.Infrastructure.Persistence.Configurations;

public class MedicineConfig : IEntityTypeConfiguration<Medicine>
{
    public void Configure(EntityTypeBuilder<Medicine> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).ValueGeneratedOnAdd();
        builder.HasIndex(m => m.RegistrationNumber).IsUnique();
    }
}
