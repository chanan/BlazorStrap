using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSOffCanvasContent : BlazorStrapBase
    {
        private string? ClassBuilder => new CssBuilder("modal-body")
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}