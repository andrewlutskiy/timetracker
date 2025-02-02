using System.Linq.Expressions;
using EmployeePortal.Api.Types;

namespace EmployeePortal.Api.DataAccess.Extensions;

public static class QueryableExtensions
{
    private const string Asc = "OrderBy";
    private const string Desc = "OrderByDescending";

    public static TResult ToPagedResult<TResult, T>(this IQueryable<T> query, int first, int rows)
        where T : class
        where TResult : IPagedResult<T>, new()
    {
        var res = new PagedResult<T>();
        ((IPagedResult<T>)res).TotalCount = query.Count();
        ((IPagedResult<T>)res).Data = query.Skip(first).Take(rows).ToList();
        return (TResult)(IPagedResult<T>)res;
    }

    public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string sortField)
    {
        return OrderByField(q, sortField, Asc);
    }

    public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string sortField, bool ascending)
    {
        return OrderByField(q, sortField, ascending ? Asc : Desc);
    }

    private static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string sortField, string orderMethod)
    {
        var param = Expression.Parameter(typeof(T), "p");
        var prop = sortField.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);
        var exp = Expression.Lambda(prop, param);

        Type[] types = { q.ElementType, exp.Body.Type };

        var mce = Expression.Call(typeof(Queryable), orderMethod, types, q.Expression, exp);

        return q.Provider.CreateQuery<T>(mce);
    }
}