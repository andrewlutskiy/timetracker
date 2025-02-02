using EmployeePortal.Api.Types.TimeLogs;

namespace EmployeePortal.Api.Models.TimeEntry;

public class TimeEntryModel
{
    public Guid Id { get; set; }

    public DateTime WorkDate { get; set; }

    public uint WorkDuration { get; set; }

    public string Description { get; set; }

    public string Comment { get; set; }

    public ProjectModel AssignedProject { get; set; }

    public TimeEntryStatus Status { get; set; }
}