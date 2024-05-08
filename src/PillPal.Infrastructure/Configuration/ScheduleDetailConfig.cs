namespace PillPal.Infrastructure.Configuration;

public class ScheduleDetailConfig : IEntityTypeConfiguration<ScheduleDetail>
{
    public void Configure(EntityTypeBuilder<ScheduleDetail> entity)
    {
        entity.ToTable("ScheduleDetail");
        entity.Property(e => e.Id).HasColumnName("ScheduleDetailId");
        entity.Property(e => e.ScheduleId).HasColumnName("ScheduleId");
    }
}
