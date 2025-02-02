using EmployeePortal.Api.Types;

namespace EmployeePortal.Api.Domain.Employees.Services;

public interface IEmployeeSearchService
{
    public Task<PagedResult<Employee>> SearchAsync(FilterParams filter);
}