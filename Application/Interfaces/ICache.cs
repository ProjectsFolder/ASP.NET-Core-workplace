namespace Application.Interfaces;

public interface ICache
{
    Task<T?> GetOrSet<T>(
        string key,
        Func<Task<T>> getter,
        int expiredSeconds = 60,
        CancellationToken? cancellationToken = default)
        where T : class;
}
