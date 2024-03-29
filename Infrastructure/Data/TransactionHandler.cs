using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class TransactionHandler(DatabaseContext context) : ITransaction
{
    public async Task<T> ExecuteAsync<T>(Func<Task<T>> func, CancellationToken? cancellationToken = null)
    {
        var strategy = context.Database.CreateExecutionStrategy();
        
        return await strategy.ExecuteAsync(async () =>
        {
            T? result;
            if (context.Database.CurrentTransaction == null)
            {
                using var transaction = await context.Database.BeginTransactionAsync(
                    cancellationToken ?? CancellationToken.None);
                try
                {
                    result = await func();
                    await transaction.CommitAsync(cancellationToken ?? CancellationToken.None);
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                result = await func();
            }

            return result;
        });
    }

    public async Task ExecuteAsync(Func<Task> action, CancellationToken? cancellationToken = null)
    {
        var strategy = context.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            if (context.Database.CurrentTransaction == null)
            {
                using var transaction = await context.Database.BeginTransactionAsync(
                    cancellationToken ?? CancellationToken.None);
                try
                {
                    await action();
                    await transaction.CommitAsync(cancellationToken ?? CancellationToken.None);
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                await action();
            }
        });
    }
}
