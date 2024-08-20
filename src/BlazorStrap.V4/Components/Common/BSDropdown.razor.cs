using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;

namespace BlazorStrap.V4
{
    public partial class BSDropdown : BSDropdownBase
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("dropdown-menu")
                .AddClass("dropdown-menu-dark", IsDark)
                .AddClass("show", Shown)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .AddClass(SyncClass)
                .Build().RemoveClassDoubles().ToNullString();

        protected override string? StyleBuilder => new StyleBuilder()
                .AddStyle(SyncStyle)
                .AddStyle(Style)
                .Build().RemoveStyleDoubles().ToNullString();

        protected override string? GroupClassBuilder => new CssBuilder()
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();

        protected override string? IsDivClassBuilder => new CssBuilder()
                .AddClass("dropdown", Parent == null)
                .AddClass("dropup", Placement is Placement.Top or Placement.TopEnd or Placement.TopStart)
                .AddClass("dropstart", Placement is Placement.Left or Placement.LeftEnd or Placement.LeftStart)
                .AddClass("dropend", Placement is Placement.Right or Placement.RightEnd or Placement.RightStart)
                .AddClass(IsDivClass)
                .Build().ToNullString();
    }
}