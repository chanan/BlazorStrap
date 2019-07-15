using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public class CodeBSTabContent : BootstrapComponentBase
    {
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
