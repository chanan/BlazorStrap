using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSNavbarBrand : BlazorStrapBase
    {
        [Parameter] public string? Url { get; set; }

        private string? ClassBuilder => new CssBuilder("navbar-brand")
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}