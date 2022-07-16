using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Content
{
    public abstract class BSTDBase : BlazorStrapBase
    {
        /// <summary>
        /// Cell content vertical alignment.
        /// </summary>
        [Parameter] public AlignRow AlignRow { get; set; }

        /// <summary>
        /// Cell background color.
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        /// <summary>
        /// Colspan of the cell.
        /// </summary>
        [Parameter] public string? ColSpan { get; set; }

        /// <summary>
        /// Whether or not the cell is active.
        /// </summary>
        [Parameter] public bool IsActive { get; set; }
        [CascadingParameter] public BSTHeadBase? TableHead { get; set; }
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
    }
}
