using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSButtonGroupBase : BlazorStrapBase
    {
        /// <summary>
        /// Sets dropdown placement.
        /// </summary>
        [Parameter] public Placement DropdownPlacement { get; set; } = Placement.BottomStart;

        /// <summary>
        /// Combine sets of button groups into button toolbars for more complex components.
        /// </summary>
        [Parameter] public bool IsToolbar { get; set; }

        /// <summary>
        /// Make a set of buttons appear vertically stacked rather than horizontally.
        /// </summary>
        [Parameter] public bool IsVertical { get; set; }
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
    }
}
