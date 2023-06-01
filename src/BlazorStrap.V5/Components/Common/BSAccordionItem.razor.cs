using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;

namespace BlazorStrap.V5
{
    public partial class BSAccordionItem : BSAccordionItemBase
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("accordion-collapse collapse")
                .AddClass("show", Shown)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .AddClass(SyncClass)
                .Build().RemoveClassDoubles().ToNullString();

        protected override string? StyleBuilder => new StyleBuilder()
                .AddStyle(SyncStyle)
                .AddStyle(Style)
                .Build().RemoveStyleDoubles().ToNullString();

    }
}