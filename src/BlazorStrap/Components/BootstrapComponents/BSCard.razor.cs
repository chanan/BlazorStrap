using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSCard : BlazorStrapBase
    {
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        internal string? ClassBuilder => new CssBuilder("card")
            .AddClass($"bs-{BSColor.GetName<BSColor>(Color).ToLower()}", Color != BSColor.Default)
            .AddClass(LayoutClass, !String.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !String.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}