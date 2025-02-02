namespace EmployeePortal.Api.Types;

public interface IPagedResult<T>
{
    int TotalCount { get; set; }

    IEnumerable<T> Data { get; set; }
}