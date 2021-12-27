using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public partial class BSNavItem : BlazorStrapBase, IDisposable
    {
        [Inject] NavigationManager NavigationManager { get; set; }

        [Parameter] public bool IsActive { get; set; }
        [Parameter] public bool IsDisabled { get; set; }
        [Parameter] public bool IsDropdown { get; set; }
        [Parameter] public bool NoNavItem { get; set; }
        [Parameter] public string? ListItemClass { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public bool PreventDefault { get; set; }
        [Parameter] public RenderFragment? TabContent { get; set; }
        [Parameter] public RenderFragment? TabLabel { get; set; }
        [Parameter] public string? Url { get; set; } = null;
        [CascadingParameter] public BSNav? Parent { get; set; }

        private string? ClassBuilder => new CssBuilder("nav-link")
            .AddClass("active", IsActive)
            .AddClass("disabled", IsDisabled)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
        
        private string? ListClassBuilder => new CssBuilder()
            .AddClass("nav-item", !NoNavItem)
            .AddClass("dropdown", IsDropdown)
            .AddClass(ListItemClass)
            .Build().ToNullString();

        protected override void OnInitialized()
        {
            if (NavigationManager.Uri == NavigationManager.BaseUri + Url)
                IsActive = true;
        }

        protected override void OnParametersSet()
        {
            if (Parent == null) return;
            if(Parent.IsTabs)
            {
                IsActive = Parent.SetFirstChild(this);
            }
            Parent.ChildHandler += Parent_ChildHandler;
        }

        private async Task ClickEvent()
        {
            IsActive = true;
            Parent?.Invoke(this);
            if (OnClick.HasDelegate)
                await OnClick.InvokeAsync();
        }

        private async void Parent_ChildHandler(BSNavItem sender)
        {
            if (sender == this) return;
            IsActive = false;
            await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            if (Parent == null) return;
            if(Parent.ActiveChild == this)
                Parent.ActiveChild = null;
            Parent.ChildHandler -= Parent_ChildHandler;
        }
    }
}