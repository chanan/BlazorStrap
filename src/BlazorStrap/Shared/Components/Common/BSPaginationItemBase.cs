using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSPaginationItemBase : BlazorStrapBase
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
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
    }
}
