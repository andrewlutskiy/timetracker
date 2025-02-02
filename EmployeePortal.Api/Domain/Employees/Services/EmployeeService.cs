using EmployeePortal.Api.DataAccess.Interfaces.Database;

namespace EmployeePortal.Api.Domain.Employees.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository<Employee?, Guid> _repository;

    public EmployeeService(IRepository<Employee?, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<Employee?> GetByEmailAsync(string email)
    {
        return _repository.GetAsync(e => e.Email == email).FirstOrDefault();
    }

    public async Task<Employee?> GetByIdAsync(Guid id)
    {
        return await _repository.GetAsync(id);
    }
}