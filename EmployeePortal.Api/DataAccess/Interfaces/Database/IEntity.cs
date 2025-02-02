namespace EmployeePortal.Api.DataAccess.Interfaces.Database;

public interface IEntity<TId>
{
    TId Id { get; set; }

    DateTime CreatedAt { get; set; }
}