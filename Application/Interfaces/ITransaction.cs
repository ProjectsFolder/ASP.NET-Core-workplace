namespace Application.Interfaces;

public interface ITransaction
{
    Task<T> ExecuteAsync<T>(Func<Task<T>> func, CancellationToken? cancellationToken = null);
}
