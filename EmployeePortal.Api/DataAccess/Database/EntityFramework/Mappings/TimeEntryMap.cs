using EmployeePortal.Api.Domain.TimeLogs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeePortal.Api.DataAccess.Database.EntityFramework.Mappings;

public class TimeEntryMap
{
    public static void Map(EntityTypeBuilder<TimeEntry> entityBuilder)
    {
        entityBuilder.Property(t => t.Id).ValueGeneratedNever();
        entityBuilder.Property(t => t.CreatedAt);
        entityBuilder.Property(t => t.Comment);
        entityBuilder.Property(t => t.Description);
        entityBuilder.Property(t => t.Status);
        entityBuilder.Property(t => t.WorkDate);
        entityBuilder.Property(t => t.WorkDuration);
        entityBuilder.HasOne(t => t.AssignedProject).WithOne().HasForeignKey<Project>("ProjectId");
    }
}