using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V4
{
    public partial class BSInputGroup : BSInputGroupBase
    {
        /// <summary>
        /// Change sizing.
        /// </summary>
        [Parameter] public Size Size { get; set; }
        [Parameter] public bool IsPrepend { get; set; }
        [Parameter] public bool IsAppend { get; set; }
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
                .AddClass("input-group-append", IsAppend && !IsPrepend)
                .AddClass("input-group-prepend", !IsAppend && IsPrepend)
                .AddClass("input-group", !IsAppend  && !IsPrepend)
                .AddClass($"input-group-{Size.ToDescriptionString()}", Size != Size.None && !IsAppend && !IsPrepend)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass) && !IsAppend && !IsPrepend)
                .AddClass(Class, !string.IsNullOrEmpty(Class) && !IsAppend && !IsPrepend)
                .Build().ToNullString();
    }
}