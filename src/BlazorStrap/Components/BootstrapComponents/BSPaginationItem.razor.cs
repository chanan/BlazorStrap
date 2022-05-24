using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSPaginationItem : BlazorStrapBase
    {
        /// <summary>
        /// Sets color of pagination item.
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        /// <summary>
        /// Sets whether the item is rendered in the active state.
        /// </summary>
        [Parameter] public bool IsActive { get; set; }

        /// <summary>
        /// Sets whether the item is rendered in the disabled state.
        /// </summary>
        [Parameter] public bool IsDisabled { get; set; }

        /// <summary>
        /// If set, turns the pagination element into a link and navigate to link on click.
        /// </summary>
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