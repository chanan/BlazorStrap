using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSSpinnerBase : BlazorStrapBase
    {
        /// <summary>
        /// Sets the spinner type.
        /// </summary>
        [Parameter] public SpinnerType SpinnerType { get; set; } = SpinnerType.Border;

        /// <summary>
        /// Sets the spinner color.
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
    }
}
