using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Static.Base;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V5_1
{
    public partial class BSSpinner : BSSpinnerBase
    {
        /// <summary>
        /// Sets the spinner size.
        /// </summary>
        [Parameter] public Size Size { get; set; } = Size.None;
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
            .AddClass("spinner-border", SpinnerType != SpinnerType.Grow)
            .AddClass("spinner-grow", SpinnerType == SpinnerType.Grow)
            .AddClass($"text-{Color.NameToLower()}", Color != BSColor.Default)
            .AddClass($"{(SpinnerType == SpinnerType.Border ? "spinner-border" : "spinner-grow")}-{Size.ToDescriptionString()}", Size != Size.None)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}