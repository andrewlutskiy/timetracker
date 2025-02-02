
using EmployeePortal.Api.Domain.TimeLogs;
using EmployeePortal.Api.Types.Entities;

namespace EmployeePortal.Api.Domain.Employees;

public class Employee : Entity<Guid>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }


    public IEnumerable<Project> AssignedProjects { get; set; }
}