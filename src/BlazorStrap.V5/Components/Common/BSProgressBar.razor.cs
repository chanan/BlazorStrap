using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;

namespace BlazorStrap.V5
{
    public partial class BSProgressBar : BSProgressBarBase
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("progress-bar")
                .AddClass($"bg-{Color.NameToLower()}", Color != BSColor.Default)
                .AddClass("progress-bar-striped", IsStriped)
                .AddClass("progress-bar-animated", IsAnimated)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();
    }
}