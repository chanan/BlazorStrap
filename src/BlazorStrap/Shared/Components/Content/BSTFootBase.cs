using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Content
{
    public abstract class BSTFootBase : BlazorStrapBase
    {
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
    }
}
