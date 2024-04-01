using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class TransactionHandler(DatabaseContext context) : ITransaction
{
    public Task<T> ExecuteAsync<T>(Func<Task<T>> func, CancellationToken? cancellationToken = null)
    {
        var strategy = context.Database.CreateExecutionStrategy();

        return strategy.ExecuteAsync(async (token) =>
        {
            T result;
            if (context.Database.CurrentTransaction == null)
            {
                using var transaction = await context.Database.BeginTransactionAsync(token);
                result = await func();
                await transaction.CommitAsync(token);
            }
            else
            {
                result = await func();
            }

            return result;
        }, cancellationToken ?? CancellationToken.None);
    }

    public Task ExecuteAsync(Func<Task> action, CancellationToken? cancellationToken = null)
    {
        var strategy = context.Database.CreateExecutionStrategy();

        return strategy.ExecuteAsync(async (token) =>
        {
            if (context.Database.CurrentTransaction == null)
            {
                using var transaction = await context.Database.BeginTransactionAsync(token);
                await action();
                await transaction.CommitAsync(token);
            }
            else
            {
                await action();
            }
        }, cancellationToken ?? CancellationToken.None);
    }
}
