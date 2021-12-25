using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSPaginationItem : BlazorStrapBase
    {
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;
        [Parameter] public bool IsActive { get; set; }

        private string? ClassBuilder => new CssBuilder("page-item")
          .AddClass($"bg-{Color.NameToLower()}", Color != BSColor.Default)
          .AddClass("active", IsActive)
          .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
          .AddClass(Class, !string.IsNullOrEmpty(Class))
          .Build().ToNullString();
    }
}