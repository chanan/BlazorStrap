using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSTabContent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        public string classname =>
            new CssBuilder("tab-content")
                .AddClass(Class)
                .Build();

        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [CascadingParameter] protected BSTab Parent { get; set; }
        protected override Task OnInitAsync()
        {
            Parent.Content = ChildContent;
            Parent.UpdateContent();
            return base.OnInitAsync();
        }
    }
}
