using EmployeePortal.Api.DataAccess.Interfaces.Database;
using EmployeePortal.Api.Domain.Employees;

namespace EmployeePortal.Api.Domain.TimeLogs.Services;

public class TimeEntryService : ITimeEntryService
{
    private readonly IRepository<TimeEntry, Guid> _timeEntryRepository;
    private readonly IRepository<Employee, Guid> _employeeRepository;


    public TimeEntryService(IRepository<TimeEntry, Guid> timeEntryRepository, IRepository<Employee, Guid> employeeRepository)
    {
        _employeeRepository = employeeRepository;
        _timeEntryRepository = timeEntryRepository;
    }

    public async Task<IEnumerable<TimeEntry>> GetUserEntries(Guid userId, DateTime fromDate, DateTime toDate)
    {
        return  _timeEntryRepository.GetAsync(e => e.UserId == userId && e.WorkDate >= fromDate && e.WorkDate <= toDate);
    }

    public async Task CreateAsync(TimeEntry entity, Guid userId)
    {
        entity.UserId= userId;
        var user = await _employeeRepository.GetWithAsync(userId, e => e.AssignedProjects);
        if (user.AssignedProjects.All(p => p.Id != entity.AssignedProject.Id))
        {
            //TODO: change to proper exception type when implemented
            throw new ArgumentException($"{entity.AssignedProject.Name} project is not assigned to the user");
        }

        await _timeEntryRepository.CreateAsync(entity);
    }
}