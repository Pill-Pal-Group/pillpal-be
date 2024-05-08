namespace PillPal.Infrastructure.Configuration;

public class DrugInformationConfig : IEntityTypeConfiguration<DrugInformation>
{
    public void Configure(EntityTypeBuilder<DrugInformation> entity)
    {
        entity.ToTable("DrugInformation");
        entity.Property(e => e.Id).HasColumnName("DrugInformationId");
        entity.Property(e => e.DrugId).HasColumnName("DrugId");
    }
}
