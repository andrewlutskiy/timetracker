using System.Linq.Expressions;
using EmployeePortal.Api.DataAccess.Interfaces.Database;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Api.DataAccess.Database.EntityFramework;

public class Repository<T, TId> : ReadOnlyRepository<T, TId>, IRepository<T, TId>
    where T : class, IEntity<TId>
{
    private readonly DatabaseContext _context;

    public Repository(DatabaseContext context)
        : base(context)
    {
        _context = context;
    }

    public virtual async Task<T> CreateAsync(T entity)
    {
        var e = await Entities.AddAsync(entity);
        SaveChanges();
        return e.Entity;
    }

    public virtual async Task DeleteAsync(T entity)
    {
        var toRemove = Entities.First(x => x.Id.Equals(entity.Id));
        Entities.Remove(toRemove);
        SaveChanges();
        await Task.CompletedTask;
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        var toRemove = Entities.First(x => x.Id.Equals(id));
        Entities.Remove(toRemove);
        SaveChanges();
        await Task.CompletedTask;
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        Entities.Update(entity);
        SaveChanges();
        return await Task.FromResult(entity);
    }

    protected virtual void SaveChanges()
    {
        _context.SaveChanges();
    }
}