using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class BSTabContentBase  : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        public string classname =>
            new CssBuilder("tab-content")
                .AddClass(Class)
                .Build();

        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [CascadingParameter] protected BSTab Parent { get; set; }
        protected override Task OnInitializedAsync()
        {
            Parent.Content = ChildContent;
            Parent.UpdateContent();
            return base.OnInitializedAsync();
        }
    }
}
