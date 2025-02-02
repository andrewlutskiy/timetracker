using EmployeePortal.Api.DataAccess.Extensions;
using EmployeePortal.Api.DataAccess.Interfaces.Database;
using EmployeePortal.Api.Types;
using EmployeePortal.Api.Utilities;

namespace EmployeePortal.Api.Domain.Employees.Services;

public class EmployeeSearchService : IEmployeeSearchService
{
    private readonly IReadOnlyRepository<Employee, Guid> _repository;

    public EmployeeSearchService(IReadOnlyRepository<Employee, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<Employee>> SearchAsync(FilterParams filter)
    {
        Argument.ExpectNotNull(filter, nameof(filter));
        Argument.ExpectGreaterThanZero(filter.RowsCount, nameof(filter.RowsCount));
        var result = _repository.GetAsync(a =>
                a.FirstName.ToUpper().Contains(string.IsNullOrWhiteSpace(filter.SearchValue) ? string.Empty : filter.SearchValue.ToUpper()))
            .OrderByField(filter.SortField, filter.SortOrder)
            .ToPagedResult<PagedResult<Employee>, Employee>(filter.First, filter.RowsCount);

        return await Task.FromResult(result);
    }
}