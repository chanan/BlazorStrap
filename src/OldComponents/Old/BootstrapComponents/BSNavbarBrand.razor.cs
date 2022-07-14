using BlazorComponentUtilities;
using BlazorStrap.Components.Base;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSNavbarBrand : LayoutBase
    {
        /// <summary>
        /// Url for brand link. See <see href="https://getbootstrap.com/docs/5.2/components/navbar/#brand">Bootstrap Documentation</see>
        /// </summary>
        [Parameter] public string? Url { get; set; }

        private string? ClassBuilder => new CssBuilder("navbar-brand")
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}