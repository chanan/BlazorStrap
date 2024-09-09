namespace BlazorStrap;

internal class FakeAsyncProvider : IAsyncProvider
{
    public Task<int> CountAsync<T>(IQueryable<T> queryable)
    {
        return Task.FromResult(queryable.Count());
    }

    public Task<T[]> ToArrayAsync<T>(IQueryable<T> queryable)
    {
        return Task.FromResult(queryable.ToArray());
    }
}

public interface IAsyncProvider
{
    Task<int> CountAsync<T>(IQueryable<T> queryable);
    Task<T[]> ToArrayAsync<T>(IQueryable<T> queryable);
}

public static class IAysncProviderExtensions
{
    public static Task<T[]> ToArrayAsync<T>(this IQueryable<T> queryable, IAsyncProvider provider)
    {
        return provider.ToArrayAsync(queryable);
    }

    public static Task<int> CountAsync<T>(this IQueryable<T> queryable, IAsyncProvider provider)
    {
        return provider.CountAsync(queryable);
    }
}
