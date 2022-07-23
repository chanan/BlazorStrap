using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;
using BlazorStrap.Utilities;

namespace BlazorStrap.V4
{
    public partial class BSAlert : BSAlertBase
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("alert")
                .AddClass($"alert-{Color.NameToLower()}", Color != BSColor.Default)
                .AddClass("d-flex align-items-center", HasIcon)
                .AddClass("alert-dismissible", IsDismissible)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();

    }
}
