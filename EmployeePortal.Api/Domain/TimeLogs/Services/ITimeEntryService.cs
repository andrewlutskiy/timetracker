namespace EmployeePortal.Api.Domain.TimeLogs.Services;

public interface ITimeEntryService
{

    Task<IEnumerable<TimeEntry>> GetUserEntries(Guid userId, DateTime fromDate, DateTime toDate);

    Task CreateAsync(TimeEntry entity, Guid userId);
}