namespace EmployeePortal.Api.Models;

public class PagedResultModel<T>
{
    public int TotalRecords { get; set; }

    public IList<T> Data { get; set; }
}