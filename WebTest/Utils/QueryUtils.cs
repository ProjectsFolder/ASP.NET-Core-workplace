using System.Linq.Expressions;

namespace WebTest.Utils
{
    public static class QueryUtils
    {
        public static IQueryable<TModel> WhereEquals<TModel, TContains>(
            this IQueryable<TModel> query,
            string propertyName,
            TContains contains)
        {
            var parameter = Expression.Parameter(typeof(TModel), "type");
            var property = Expression.Property(parameter, propertyName);
            var method = typeof(TContains).GetMethod("Equals", [typeof(TContains)]);
            var someValue = Expression.Constant(contains, typeof(TContains));
            var compare = Expression.Call(property, method, someValue);

            return query.Where(Expression.Lambda<Func<TModel, bool>>(compare, parameter));
        }
    }
}
