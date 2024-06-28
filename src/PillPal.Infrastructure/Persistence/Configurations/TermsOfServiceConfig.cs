namespace PillPal.Infrastructure.Persistence.Configurations;

public class TermsOfServiceConfig : IEntityTypeConfiguration<TermsOfService>
{
    public void Configure(EntityTypeBuilder<TermsOfService> builder)
    {
        builder.HasKey(tos => tos.Id);
        builder.Property(tos => tos.Id).ValueGeneratedOnAdd();
    }
}
