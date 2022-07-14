using BlazorStrap.Shared.Components.Static.Base;

namespace BlazorStrap.V5_1
{
    public partial class BSAlertLink : BSAlertLinkBase
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => null;
    }
}