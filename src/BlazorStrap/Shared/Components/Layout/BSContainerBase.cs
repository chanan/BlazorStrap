namespace BlazorStrap.Shared.Components.Layout
{
    public abstract class BSContainerBase : BlazorStrapBase
    {
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
    }
}
