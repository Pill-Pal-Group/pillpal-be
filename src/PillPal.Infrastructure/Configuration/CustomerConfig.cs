namespace PillPal.Infrastructure.Configuration;

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity.ToTable("Customer");
        entity.Property(e => e.Id).HasColumnName("CustomerId");
        entity.Property(e => e.IdentityUserId).HasColumnName("AccountId");

        entity.HasIndex(e => e.CustomerCode).IsUnique();
        entity.HasIndex(e => e.IdentityUserId).IsUnique();

        entity.HasOne(d => d.IdentityUser)
            .WithOne(d => d.Customer)
            .HasForeignKey<Customer>(d => d.IdentityUserId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(d => d.CustomerPackages)
            .WithOne(p => p.Customer)
            .HasForeignKey(d => d.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(d => d.Prescripts)
            .WithOne(p => p.Customer)
            .HasForeignKey(d => d.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(d => d.Schedules)
            .WithOne(p => p.Customer)
            .HasForeignKey(d => d.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
