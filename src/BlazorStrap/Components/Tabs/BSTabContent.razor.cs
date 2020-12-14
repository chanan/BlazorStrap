using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public partial class BSTabContent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        public string Classname =>
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
