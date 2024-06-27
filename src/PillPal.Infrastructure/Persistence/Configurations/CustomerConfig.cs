namespace PillPal.Infrastructure.Persistence.Configurations;

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        builder.Property(c => c.BreakfastTime)
            .HasDefaultValue(new TimeOnly(7, 0, 0));

        builder.Property(c => c.LunchTime)
            .HasDefaultValue(new TimeOnly(12, 0, 0));   

        builder.Property(c => c.AfternoonTime)
            .HasDefaultValue(new TimeOnly(16, 0, 0));

        builder.Property(c => c.DinnerTime)
            .HasDefaultValue(new TimeOnly(18, 0, 0));

        builder.Property(c => c.MealTimeOffset)
            .HasDefaultValue(new TimeOnly(0, 15, 0));
    }
}
