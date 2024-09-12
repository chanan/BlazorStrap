// This file is an adaptation of code from Microsoft.AspNetCore.Components.QuickGrid
// The original code is licensed under the MIT License by the .NET Foundation.
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorStrap.Shared.Components.DataGrid;

public class AsyncProviderSupplier
{
    private static readonly ConcurrentDictionary<Type, bool> IsEntityFrameworkProviderTypeCache = new();

    public static IAsyncProvider? GetAsyncQueryExecutor<T>(IServiceProvider services, IQueryable<T>? queryable)
    {
        if (queryable is not null)
        {
            var executor = services.GetService<IAsyncProvider>();

            if (executor is not null &&  executor.IsSupported(queryable))
            {
                return executor;
            }
        }

        return null;
    }

    private static bool IsEntityFrameworkProviderType([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] Type queryableProviderType)
        => queryableProviderType.GetInterfaces().Any(x => string.Equals(x.FullName, "Microsoft.EntityFrameworkCore.Query.IAsyncQueryProvider", StringComparison.Ordinal)) == true;
}