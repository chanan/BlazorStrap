namespace BlazorStrap_Docs.SamplesHelpers.Content.Tables;

public static class FakeEfCore
{
    public static Task<List<T>> ToListAsync<T>(this IEnumerable<T> source)
    {
        return Task.FromResult(source.ToList());
    }
}