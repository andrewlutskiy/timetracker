using EmployeePortal.Api.Domain.Employees;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeePortal.Api.DataAccess.Database.EntityFramework.Mappings;

public class EmployeeMap
{
    public static void Map(EntityTypeBuilder<Employee> entityBuilder)
    {
        entityBuilder.Property(t => t.Id).ValueGeneratedNever();
        entityBuilder.Property(t => t.FirstName);
        entityBuilder.Property(t => t.LastName);
        entityBuilder.Property(t => t.Email);
        entityBuilder.HasMany(t => t.AssignedProjects).WithMany(t => t.AssignedTo);
    }
}