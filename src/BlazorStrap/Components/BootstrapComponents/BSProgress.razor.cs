using BlazorComponentUtilities;

namespace BlazorStrap
{
    public partial class BSProgress : BlazorStrapBase
    {
        internal int Children { get; set; }

        internal string? ClassBuilder => new CssBuilder("progress")
         .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
         .AddClass(Class, !string.IsNullOrEmpty(Class))
         .Build().ToNullString();

        internal void Refresh()
        {
            StateHasChanged();
        }
    }
}