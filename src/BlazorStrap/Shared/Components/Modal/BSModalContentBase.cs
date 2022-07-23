using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Modal
{
    public abstract class BSModalContentBase : BlazorStrapBase
    {
        /// <summary>
        /// Modal Color
        /// </summary>
        [Parameter] public BSColor ModalColor { get; set; } = BSColor.Default;
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
    }
}
