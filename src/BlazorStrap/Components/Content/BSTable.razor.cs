using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSTable : BlazorStrapBase
    {
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;
        [Parameter] public bool IsDark { get; set; }
        [Parameter] public bool IsHoverable { get; set; }
        [Parameter] public bool IsResponsive { get; set; }
        [Parameter] public Size ResponsiveSize { get; set; } = Size.None;
        [Parameter] public bool IsSmall { get; set; }
        [Parameter] public bool IsBordered { get; set; }
        [Parameter] public bool IsBorderLess { get; set; }
        [Parameter] public bool IsCaptionTop { get; set; }
        [Parameter] public bool IsStriped { get; set; }

        internal string? ClassBuilder => new CssBuilder("table")
         .AddClass("table-striped", IsStriped)
         .AddClass("table-dark", IsDark)
         .AddClass("table-hover", IsHoverable)
         .AddClass("table-sm", IsSmall)
         .AddClass("table-bordered", IsBordered)
         .AddClass("table-borderless", IsBorderLess)
         .AddClass($"table-{Color.NameToLower()}", Color != BSColor.Default)
         .AddClass("caption-top", IsCaptionTop)
         .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
         .AddClass(Class, !string.IsNullOrEmpty(Class))
         .Build().ToNullString();

        internal string? WrapperClassBuilder => new CssBuilder("bs-table-responsive")
            .AddClass("table-responsive", ResponsiveSize == Size.None)
            .AddClass($"table-responsive-{ResponsiveSize.ToDescriptionString()}", ResponsiveSize != Size.None)
            .Build().ToNullString();
            
    }
}
