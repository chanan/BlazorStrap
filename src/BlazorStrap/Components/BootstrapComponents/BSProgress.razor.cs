using BlazorComponentUtilities;

namespace BlazorStrap
{
    public partial class BSProgress : BlazorStrapBase
    {
        internal List<BSProgressBar> Children { get; set; } = new List<BSProgressBar>();
        internal Action? NotifyChildren { get; set; }

        private string? ClassBuilder => new CssBuilder("progress")
         .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
         .AddClass(Class, !string.IsNullOrEmpty(Class))
         .Build().ToNullString();

        internal void AddChild(BSProgressBar child)
        {
            Children.Add(child);
            NotifyChildren?.Invoke();
        }

        internal void RemoveChild(BSProgressBar child)
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