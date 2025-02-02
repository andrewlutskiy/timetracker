using EmployeePortal.Api.Domain.Employees.Services;
using EmployeePortal.Api.Domain.TimeLogs.Services;
using EmployeePortal.Api.Models.TimeEntry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using EmployeePortal.Api.Domain.TimeLogs;
using EmployeePortal.Api.Types.TimeLogs;

namespace EmployeePortal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "AuthPolicy")]
public class TimeEntryController : Controller
{
    private readonly IEmployeeService _employeeService;
    private readonly ITimeEntryService _timeEntryService;

    public TimeEntryController(IEmployeeService employeeService, ITimeEntryService timeEntryService)
    {
        _employeeService = employeeService;
        _timeEntryService = timeEntryService;
    }

    [HttpGet()]
    public async Task<IEnumerable<TimeEntryModel>> GetCurrentUserEntires([FromQuery]DateTime fromDate, [FromQuery]DateTime toDate)
    {
        var user = await _employeeService.GetByEmailAsync(this.User.Identity.Name);
        var entries = await _timeEntryService.GetUserEntries(user.Id, fromDate, toDate);
        return entries.Select(e => new TimeEntryModel
        {
            AssignedProject = new ProjectModel
            {
                Id = e.AssignedProject.Id,
                Name = e.AssignedProject.Name
            },
            Comment = e.Comment,
            Id = e.Id,
            Description = e.Description,
            Status = e.Status,
            WorkDate = e.WorkDate,
            WorkDuration = e.WorkDuration
        });
    }

    [HttpPut()]
    public async Task CreateNewEntires([FromBody]TimeEntryModel model)
    {
        var user = await _employeeService.GetByEmailAsync(this.User.Identity.Name);
        _timeEntryService.CreateAsync(new TimeEntry
            {
                WorkDuration = model.WorkDuration,
                Description = model.Description,
                Status = TimeEntryStatus.Pending,
                WorkDate = model.WorkDate,
                AssignedProject = new Project
                {
                    Id = model.AssignedProject.Id,
                    Name = model.AssignedProject.Name
                },
                CreatedAt = TimeProvider.System.GetUtcNow().DateTime
            },
            user.Id);
    }
}