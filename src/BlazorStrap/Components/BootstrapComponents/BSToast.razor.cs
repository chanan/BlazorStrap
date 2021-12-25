using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public partial class BSToast : BlazorStrapBase
    {
        [Parameter] public string ButtonClass { get; set; }

        [Parameter] public BSColor Color { get; set; } = BSColor.Default;
        [Parameter] public RenderFragment Content { get; set; }
        [Parameter] public string ContentClass { get; set; }
        
        [Parameter] public RenderFragment Header { get; set; }
        [Parameter] public string HeaderClass { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        private string? ClassBuilder => new CssBuilder("toast")
            .AddClass("show", Shown)
            .AddClass("fade")
            .AddClass($"bg-{BSColor.GetName<BSColor>(Color).ToLower()}", Color != BSColor.Default)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private string? ContentClassBuilder => new CssBuilder("toast-body")
           .AddClass(ContentClass)
           .Build().ToNullString();

        private string? HeaderClassBuilder => new CssBuilder("toast-header")
           .AddClass(HeaderClass)
           .Build().ToNullString();

        private bool Shown { get; set; } = true;

        private async Task ClickEvent()
        {
            if (!OnClick.HasDelegate)
                Toggle();
            await OnClick.InvokeAsync();
        }

        public void Toggle()
        {
            Shown = !Shown;
        }
    }
}