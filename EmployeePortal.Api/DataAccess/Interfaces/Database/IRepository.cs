namespace EmployeePortal.Api.DataAccess.Interfaces.Database;

public interface IRepository<T, in TId> : IReadOnlyRepository<T, TId>
    where T : class, IEntity<TId>
{
    Task<T> CreateAsync(T entity);

    Task<T> UpdateAsync(T entity);

    Task DeleteAsync(T entity);

    Task DeleteAsync(Guid id);
}