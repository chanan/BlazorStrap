using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Content;

namespace BlazorStrap.V4
{
    public partial class BSTR : BSTRBase
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
                .AddClass("table-active", IsActive)
                .AddClass($"table-{Color.NameToLower()}", Color != BSColor.Default)
                .AddClass($"align-{AlignRow.NameToLower()}", AlignRow != AlignRow.Default)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();
    }
}