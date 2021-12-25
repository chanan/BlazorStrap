using BlazorComponentUtilities;

using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSPagination : BlazorStrapBase
    {
        [Parameter] public Align Align { get; set; } = Align.Default;

        [Parameter] public BSColor Color { get; set; } = BSColor.Default;
        [Parameter] public Size Size { get; set; } = Size.None;

        private string? ClassBuilder => new CssBuilder("pagination")
          .AddClass($"bg-{Color.NameToLower()}", Color != BSColor.Default)
          .AddClass($"pagination-{Size.ToDescriptionString()}", Size != Size.None)
          .AddClass($"justify-content-{Align.NameToLower()}", Align != Align.Default)
          .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
          .AddClass(Class, !string.IsNullOrEmpty(Class))
          .Build().ToNullString();
    }
}