// This file is an adaptation of code from Microsoft.AspNetCore.Components.QuickGrid.EntityFrameworkAdapter.
// The original code is licensed under the MIT License by the .NET Foundation.

using BlazorStrap;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace BlazorStrap_Docs.Service;

public class AsyncProvider : IAsyncProvider
{
    public bool IsSupported<T>(IQueryable<T> queryable)
        => queryable.Provider is IAsyncQueryProvider;
    
    public Task<int> CountAsync<T>(IQueryable<T> queryable)
    {
        return queryable.CountAsync();
    }

    public Task<T[]> ToArrayAsync<T>(IQueryable<T> queryable)
    {
        return queryable.ToArrayAsync();
    }
}