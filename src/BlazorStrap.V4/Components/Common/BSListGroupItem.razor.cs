using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;

namespace BlazorStrap.V4
{
    public partial class BSListGroupItem : BSListGroupItemBase
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("list-group-item")
                .AddClass($"list-group-item-action", !string.IsNullOrEmpty(Url) || IsButton)
                .AddClass($"list-group-item-{Color.NameToLower()}", Color != BSColor.Default)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();
    }
}