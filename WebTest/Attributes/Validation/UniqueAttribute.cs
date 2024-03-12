using System.ComponentModel.DataAnnotations;
using WebTest.Models;
using WebTest.Services.Database;
using WebTest.Utils;

namespace WebTest.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UniqueAttribute<T>(
        string fieldName,
        string? routeIdKey = default) : ValidationAttribute
        where T : ModelBase
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return ValidationResult.Success;
            }

            var db = validationContext.GetService<DatabaseContext>();
            if (db == null)
            {
                return new ValidationResult("Database not found");
            }

            var set = db.Set<T>();
            var query = set.WhereEquals(fieldName, value);
            if (routeIdKey != null)
            {
                var http = validationContext.GetService<IHttpContextAccessor>();
                if (http == null || http.HttpContext == null)
                {
                    return new ValidationResult("Http not found");
                }

                if (
                    http.HttpContext.Request.RouteValues[routeIdKey] is string id
                    && int.TryParse(id, out int userId))
                {
                    query = query.Where(e => e.Id != userId);
                }
            }
            if (query.Any())
            {
                return new ValidationResult(string.Format("The '{0}' already exist", value));
            }

            return ValidationResult.Success;
        }
    }
}
