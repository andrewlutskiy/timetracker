using System.Linq.Expressions;
using EmployeePortal.Api.DataAccess.Database.Exceptions;
using EmployeePortal.Api.DataAccess.Interfaces.Database;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Api.DataAccess.Database.EntityFramework;

public class ReadOnlyRepository<T, TId> : IReadOnlyRepository<T, TId>
    where T : class, IEntity<TId>
{
    private readonly DatabaseContext _context;

    public ReadOnlyRepository(DatabaseContext context)
    {
        _context = context;
        Entities = context.Set<T>();
    }

    protected DbSet<T> Entities { get; }

    public IQueryable<T> GetAsync(Expression<Func<T, bool>> expression)
    {
        return Entities.Where(expression);
    }

    public IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> expr)
    {
        return Entities.Include(expr);
    }

    public Task<T> GetWithAsync<TProperty>(TId id, Expression<Func<T, TProperty>> subEntity)
    {
        return Entities.Include(subEntity).SingleOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<IEnumerable<T>> GetWithAsync<TProperty>(Expression<Func<T, TProperty>> subEntity)
    {
        return await Entities.Include(subEntity).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetWithAsync<TProperty>(
        Expression<Func<T, bool>> expression,
        Expression<Func<T, TProperty>> subEntity)
    {
        return await Entities.Where(expression).Include(subEntity).ToArrayAsync();
    }

    public async Task<T> GetSingleWithAsync<TProperty>(
        Expression<Func<T, bool>> expression,
        Expression<Func<T, TProperty>> subEntity)
    {
        return await Entities.Where(expression).Include(subEntity).FirstAsync();
    }

    public async Task<IEnumerable<T>> GetAsync(IEnumerable<TId> ids)
    {
        return await Entities.Where(x => ids.Contains(x.Id)).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Entities.OrderByDescending(_ => _.CreatedAt).ToListAsync();
    }

    public async Task<T> GetAsync(TId id)
    {
        var result = await Entities.SingleOrDefaultAsync(x => x.Id.Equals(id));
        if (result == null)
        {
            throw new EntityNotFoundException($"Entity with the Id {id} was not found in the database");
        }

        return result;
    }

    public async Task<bool> Exists(TId id)
    {
        return await Entities.AnyAsync(x => x.Id.Equals(id));
    }
}