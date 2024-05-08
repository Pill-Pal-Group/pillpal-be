namespace PillPal.Infrastructure.Configuration;

public class ScheduleConfig : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> entity)
    {
        entity.ToTable("Schedule");
        entity.Property(e => e.Id).HasColumnName("ScheduleId");
        entity.Property(e => e.CustomerId).HasColumnName("CustomerId");

        entity.HasMany(d => d.ScheduleDetails)
            .WithOne(p => p.Schedule)
            .HasForeignKey(d => d.ScheduleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
