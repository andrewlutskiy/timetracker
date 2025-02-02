using EmployeePortal.Api.Domain.TimeLogs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeePortal.Api.DataAccess.Database.EntityFramework.Mappings;

public class ProjectMap
{
    public static void Map(EntityTypeBuilder<Project> entityBuilder)
    {
        entityBuilder.Property(t => t.Id).ValueGeneratedNever();
        entityBuilder.Property(t => t.Name);
    }
}