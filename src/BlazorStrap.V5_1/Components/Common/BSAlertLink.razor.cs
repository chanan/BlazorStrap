using BlazorStrap.Shared.Components.Common;

namespace BlazorStrap.V5_1
{
    public partial class BSAlertLink : BSAlertLinkBase
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => null;
    }
}