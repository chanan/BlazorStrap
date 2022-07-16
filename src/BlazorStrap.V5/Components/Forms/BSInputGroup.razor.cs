using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V5
{
    public partial class BSInputGroup : BSInputGroupBase
    {
        /// <summary>
        /// Change sizing.
        /// </summary>
        [Parameter] public Size Size { get; set; }
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
                .AddClass("input-group")
                .AddClass($"input-group-{Size.ToDescriptionString()}", Size != Size.None)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();
    }
}