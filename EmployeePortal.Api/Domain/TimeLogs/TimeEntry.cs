using EmployeePortal.Api.Types.Entities;
using EmployeePortal.Api.Types.TimeLogs;

namespace EmployeePortal.Api.Domain.TimeLogs;

public class TimeEntry : Entity<Guid>
{
    public Guid UserId { get; set; }

    public DateTime WorkDate { get; set; }

    public uint WorkDuration { get; set; }

    public string Description { get; set; }

    public string Comment { get; set; }

    public Project AssignedProject { get; set; }

    public TimeEntryStatus Status { get; set; }
}