using System.Linq.Expressions;
using WebTest.Services.Database.Dto;

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

        public static Paginator<TModel> GetPaginator<TModel>(
            this IQueryable<TModel> query, int page, int pageSize)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 1;
            }

            var total = query.Count();
            var totalPages = (int)Math.Ceiling((float)total / pageSize);
            if (page > totalPages)
            {
                page = totalPages;
            }

            var result = query.Skip((page - 1) * pageSize).Take(pageSize);

            var paginator = new Paginator<TModel>
            {
                Data = result,
                CurrentPage = page,
                PerPage = pageSize,
                Total = total,
                TotalPages = totalPages,
            };

            return paginator;
        }
    }
}
