using BlazorStrap.Shared.Components.Common;

namespace BlazorStrap.V4
{
    public partial class BSAlertLink : BSAlertLinkBase
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => null;
    }
}