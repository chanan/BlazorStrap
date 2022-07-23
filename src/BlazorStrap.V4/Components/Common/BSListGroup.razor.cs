using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;

namespace BlazorStrap.V4
{
    public partial class BSListGroup : BSListGroupBase<Size>
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("list-group")
                .AddClass($"list-group-flush", IsFlush)
                .AddClass($"list-group-numbered", IsNumbered)
                .AddClass($"list-group-horizontal", IsHorizontal && Size == Size.None)
                .AddClass($"list-group-horizontal-{Size.ToDescriptionString()}", IsHorizontal && Size != Size.None)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();
    }
}