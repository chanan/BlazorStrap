namespace BlazorStrap;

internal class FakeAsyncProvider : IAsyncProvider
{
    public bool IsSupported<T>(IQueryable<T> queryable)
    {
        return true;
    }
    public Task<int> CountAsync<T>(IQueryable<T> queryable)
    {
        return Task.FromResult(queryable.Count());
    }

    public Task<T[]> ToArrayAsync<T>(IQueryable<T> queryable)
    {
        return Task.FromResult(queryable.ToArray());
    }
}