using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSBreadcrumb : BlazorStrapBase
    {
        [Parameter] public string Divider { get; set; } = "'/'";

        private string? ClassBuilder => new CssBuilder("breadcrumb")
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}
