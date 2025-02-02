using EmployeePortal.Api.Domain.Employees;
using EmployeePortal.Api.Types.Entities;

namespace EmployeePortal.Api.Domain.TimeLogs;

public class Project : Entity<Guid>
{
    public string Name { get; set; }

    public IEnumerable<Employee> AssignedTo { get; set; }
}