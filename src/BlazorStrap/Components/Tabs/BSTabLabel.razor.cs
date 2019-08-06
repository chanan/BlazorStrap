using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSTabLabel : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
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
