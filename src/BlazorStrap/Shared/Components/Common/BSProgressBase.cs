namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSProgressBase : BlazorStrapBase
    {
        internal List<BSProgressBarBase> Children { get; set; } = new List<BSProgressBarBase>();
        internal Action? NotifyChildren { get; set; }

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
        internal void AddChild(BSProgressBarBase child)
        {
            Children.Add(child);
            NotifyChildren?.Invoke();
        }

        internal void RemoveChild(BSProgressBarBase child)
        {
            Children.Remove(child);
            NotifyChildren?.Invoke();
        }
        internal void Refresh()
        {
            StateHasChanged();
        }
    }
}
