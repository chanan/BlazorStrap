using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;

namespace BlazorStrap.V4
{
    public partial class BSBreadcrumb : BSBreadcrumbBase
    {
        private int _skip = 0;
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("breadcrumb")
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();
        
        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (Tree.Count > MaxItems)
            {
                _skip = Tree.Count - MaxItems;
            }
        }
    }
}
