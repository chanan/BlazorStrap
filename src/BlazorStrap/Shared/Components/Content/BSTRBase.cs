using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Content
{
    public abstract class BSTRBase : BlazorStrapBase
    {
        /// <summary>
        /// Content alignment within the row.
        /// </summary>
        [Parameter] public AlignRow AlignRow { get; set; }

        /// <summary>
        /// Row background color.
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        /// <summary>
        /// Whether or not the row is active.
        /// </summary>
        [Parameter] public bool IsActive { get; set; }
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
    }
}
