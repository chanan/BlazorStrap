using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSModalFooter : BlazorStrapBase
    {
        private string? ClassBuilder => new CssBuilder("modal-footer")
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}