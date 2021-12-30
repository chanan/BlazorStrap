using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSPaginationItem : BlazorStrapBase
    {
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;
        [Parameter] public bool IsActive { get; set; }
        [Parameter] public bool IsDisabled { get; set; }
        [Parameter] public string? Url { get; set; }
        

        private string? ClassBuilder => new CssBuilder("page-item")
          .AddClass($"bg-{Color.NameToLower()}", Color != BSColor.Default)
          .AddClass("active", IsActive)
          .AddClass("disabled", IsDisabled)
          .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
          .AddClass(Class, !string.IsNullOrEmpty(Class))
          .Build().ToNullString();
    }
}