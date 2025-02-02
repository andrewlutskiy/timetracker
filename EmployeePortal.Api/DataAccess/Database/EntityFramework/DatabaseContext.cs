using EmployeePortal.Api.DataAccess.Database.EntityFramework.Mappings;
using EmployeePortal.Api.Domain.Employees;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Api.DataAccess.Database.EntityFramework;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        EmployeeMap.Map(modelBuilder.Entity<Employee>());
    }
}