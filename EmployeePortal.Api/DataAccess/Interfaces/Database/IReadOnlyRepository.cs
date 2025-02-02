using System.Linq.Expressions;

namespace EmployeePortal.Api.DataAccess.Interfaces.Database;

public interface IReadOnlyRepository<T, in TId>
    where T : class, IEntity<TId>
{
    Task<T> GetAsync(TId id);

    IQueryable<T> GetAsync(Expression<Func<T, bool>> expression);

    IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> entities);

    Task<IEnumerable<T>> GetAsync(IEnumerable<TId> ids);

    Task<IEnumerable<T>> GetAllAsync();
    
    Task<bool> Exists(TId id);

    Task<T> GetWithAsync<TProperty>(TId id, Expression<Func<T, TProperty>> subEntity);

    Task<IEnumerable<T>> GetWithAsync<TProperty>(Expression<Func<T, TProperty>> subEntity);

    Task<IEnumerable<T>> GetWithAsync<TProperty>(
        Expression<Func<T, bool>> expression,
        Expression<Func<T, TProperty>> subEntity);

    Task<T> GetSingleWithAsync<TProperty>(
        Expression<Func<T, bool>> expression,
        Expression<Func<T, TProperty>> subEntity);
}