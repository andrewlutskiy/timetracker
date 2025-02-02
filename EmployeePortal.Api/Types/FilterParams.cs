namespace EmployeePortal.Api.Types;

public class FilterParams
{
    public int First { get; set; }

    public int RowsCount { get; set; }

    public bool SortOrder { get; set; }

    public string SortField { get; set; }


    public string FilterField { get; set; }

    public string SearchValue { get; set; }
}