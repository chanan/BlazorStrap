using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSTable : BlazorStrapBase
    {
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;
        [Parameter] public bool IsBordered { get; set; }
        [Parameter] public bool IsBorderLess { get; set; }
        [Parameter] public bool IsCaptionTop { get; set; }
        [Parameter] public bool IsStriped { get; set; }

        private string? ClassBuilder => new CssBuilder("table")
         .AddClass("table-striped", IsStriped)
         .AddClass("table-bordered", IsBordered)
         .AddClass("table-borderless", IsBorderLess)
         .AddClass($"table-{Color.NameToLower()}" , Color != BSColor.Default)
         .AddClass("caption-top", IsCaptionTop)
         .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
         .AddClass(Class, !string.IsNullOrEmpty(Class))
         .Build().ToNullString();
    }
}
