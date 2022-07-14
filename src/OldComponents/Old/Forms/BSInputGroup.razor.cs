using BlazorComponentUtilities;
using BlazorStrap.Bootstrap.V5_1.Enums;
using BlazorStrap.Components.Base;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSInputGroup : LayoutBase
    {
        /// <summary>
        /// Change sizing.
        /// </summary>
        [Parameter] public Size Size { get; set; }

        private string? ClassBuilder => new CssBuilder()
            .AddClass("input-group")
            .AddClass($"input-group-{Size.ToDescriptionString()}", Size != Size.None)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}
