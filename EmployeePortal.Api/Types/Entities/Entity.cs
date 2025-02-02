using EmployeePortal.Api.DataAccess.Interfaces.Database;

namespace EmployeePortal.Api.Types.Entities;

public abstract class Entity<TId> : IEntity<TId>
{
    protected Entity()
    {
        this.CreatedAt = DateTime.UtcNow;
    }

    public TId Id { get; set; }

    public DateTime CreatedAt { get; set; }
}