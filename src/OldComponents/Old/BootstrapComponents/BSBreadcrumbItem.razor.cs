using BlazorComponentUtilities;
using BlazorStrap.Components.Base;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSBreadcrumbItem : LayoutBase
    {
        /// <summary>
        /// Whether or not the breadcrumb item is active.
        /// </summary>
        [Parameter] public bool IsActive { get; set; }

        /// <summary>
        /// Link for breadcrumb item.
        /// </summary>
        [Parameter] public string? Url { get; set; }

        private string? ClassBuilder => new CssBuilder("breadcrumb-item")
            .AddClass("active", IsActive)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}
