namespace EmployeePortal.Api.DataAccess.Database;

public class PostgresDbOptions
{
    public string HostName { get; set; } = default!;
    public int Port { get; set; } = default!;
    public string DatabaseName { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string UserPassword { get; set; } = default!;
    public int Timeout { get; set; } = default!;

    public string BuildConnectionString()
    {
        return $"Host={this.HostName};Port={this.Port};Database={this.DatabaseName};Username={this.UserName};Password={this.UserPassword};Timeout={this.Timeout};";
    }
}