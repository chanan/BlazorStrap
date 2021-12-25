using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSBreadcrumbItem : BlazorStrapBase
    {
        [Parameter] public bool IsActive { get; set; }
        [Parameter] public string? Url { get; set; }

        private string? ClassBuilder => new CssBuilder("breadcrumb-item")
            .AddClass("active", IsActive)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}
