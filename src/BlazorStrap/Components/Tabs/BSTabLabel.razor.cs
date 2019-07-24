using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;

namespace BlazorStrap
{
    public class CodeBSTabLabel : BootstrapComponentBase
    {
        public RenderFragment Content { get; set; }
        protected string classname =>
        new CssBuilder("nav-item nav-link")
            .AddClass("active", (Parent != null) ? Parent.Selected : false)
            .AddClass("disabled", IsDisabled)
            .AddClass(Class)
        .Build();

        [CascadingParameter] protected BSTab Parent { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected bool IsDisabled { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }

        protected void Select()
        {
            Parent.Select();
        }
    }
}
