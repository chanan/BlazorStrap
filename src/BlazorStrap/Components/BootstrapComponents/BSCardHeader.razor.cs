using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSCardHeader : BlazorStrapBase
    {
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        private string? ClassBuilder => new CssBuilder("card-header")
          .AddClass($"bg-{BSColor.GetName<BSColor>(Color).ToLower()}", Color != BSColor.Default)
          .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
          .AddClass(Class, !string.IsNullOrEmpty(Class))
          .Build().ToNullString();
    }
}