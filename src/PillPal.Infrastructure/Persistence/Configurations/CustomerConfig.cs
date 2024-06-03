namespace PillPal.Infrastructure.Persistence.Configurations;

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        //entity.Property(e => e.IdentityUserId).HasColumnName("AccountId");

        //entity.HasIndex(e => e.CustomerCode).IsUnique();
        //entity.HasIndex(e => e.IdentityUserId).IsUnique();

        //entity.HasOne(d => d.IdentityUser)
        //    .WithOne(d => d.Customer)
        //    .HasForeignKey<Customer>(d => d.IdentityUserId)
        //    .OnDelete(DeleteBehavior.Restrict);

        //entity.HasMany(d => d.CustomerPackages)
        //    .WithOne(p => p.Customer)
        //    .HasForeignKey(d => d.CustomerId)
        //    .OnDelete(DeleteBehavior.Restrict);

        //entity.HasMany(d => d.Prescripts)
        //    .WithOne(p => p.Customer)
        //    .HasForeignKey(d => d.CustomerId)
        //    .OnDelete(DeleteBehavior.Restrict);

        //entity.HasMany(d => d.Schedules)
        //    .WithOne(p => p.Customer)
        //    .HasForeignKey(d => d.CustomerId)
        //    .OnDelete(DeleteBehavior.Restrict);
    }
}
