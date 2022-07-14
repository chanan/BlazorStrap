using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Static.Base
{
    public abstract class BSBadgeBase : BlazorStrapBase
    {
        /// <summary>
        /// Color class of badge
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        /// <summary>
        /// Whether or not the badge is styled as a pill.
        /// </summary>
        [Parameter] public bool IsPill { get; set; }

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
    }
}
