using EmployeePortal.Api.Domain.Employees.Services;

namespace EmployeePortal.Api.Bootstraper;

public static class DomainBootstrap
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeSearchService, EmployeeSearchService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
    }
}