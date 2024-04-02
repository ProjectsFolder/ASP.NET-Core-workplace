using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data;

public static class Dependency
{
    public static IServiceCollection AddDatabase(
    this IServiceCollection services,
    string connectionString)
    {
        services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<ITransaction, TransactionHandler>();

        return services;
    }
}
