namespace EmployeePortal.Api.Types;

public class PagedResult<T> : IPagedResult<T>
{
    public int TotalCount { get; set; }

    public IEnumerable<T> Data { get; set; }
}