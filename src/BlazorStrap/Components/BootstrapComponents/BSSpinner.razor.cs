using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSSpinner : BlazorStrapBase
    {
        [Parameter] public SpinnerType SpinnerType { get; set; } = SpinnerType.Border;
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;
        [Parameter] public Size Size { get; set; } = Size.None;
 
        private string? ClassBuilder => new CssBuilder()
            .AddClass("spinner-border", SpinnerType != SpinnerType.Grow)
            .AddClass($"text-{Color.NameToLower()}", Color != BSColor.Default)
            .AddClass($"{(SpinnerType == SpinnerType.Border ? "spinner-border" : "spinner-grow")}-{Size.ToDescriptionString()}", Size != Size.None)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

    }
}