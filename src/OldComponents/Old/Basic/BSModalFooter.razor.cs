using BlazorComponentUtilities;
using BlazorStrap.Components.Base;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSModalFooter : LayoutBase
    {
        private string? ClassBuilder => new CssBuilder("modal-footer")
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}