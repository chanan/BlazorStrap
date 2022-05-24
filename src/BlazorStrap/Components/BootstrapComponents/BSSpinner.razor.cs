using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSSpinner : BlazorStrapBase
    {
        /// <summary>
        /// Sets the spinner type.
        /// </summary>
        [Parameter] public SpinnerType SpinnerType { get; set; } = SpinnerType.Border;

        /// <summary>
        /// Sets the spinner color.
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        /// <summary>
        /// Sets the spinner size.
        /// </summary>
        [Parameter] public Size Size { get; set; } = Size.None;

        private string? ClassBuilder => new CssBuilder()
            .AddClass("spinner-border", SpinnerType != SpinnerType.Grow)
            .AddClass("spinner-grow", SpinnerType == SpinnerType.Grow)
            .AddClass($"text-{Color.NameToLower()}", Color != BSColor.Default)
            .AddClass($"{(SpinnerType == SpinnerType.Border ? "spinner-border" : "spinner-grow")}-{Size.ToDescriptionString()}", Size != Size.None)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}