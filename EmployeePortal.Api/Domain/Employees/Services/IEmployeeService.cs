namespace EmployeePortal.Api.Domain.Employees.Services;

public interface IEmployeeService
{
    Task<Employee?> GetByEmailAsync(string email);

    Task<Employee?> GetByIdAsync(Guid id);
}