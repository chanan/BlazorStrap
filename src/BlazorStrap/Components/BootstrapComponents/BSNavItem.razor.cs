using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public partial class BSNavItem : BlazorStrapBase, IDisposable
    {
        // ReSharper disable once NullableWarningSuppressionIsUsed
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Parameter] public string? Target { get; set; }
        [Parameter] public bool? IsActive { get; set; }
        [Parameter] public bool IsDisabled { get; set; }
        [Parameter] public bool IsDropdown { get; set; }
        [Parameter] public bool NoNavItem { get; set; }
        [Parameter] public bool ActiveOnChildRoutes { get; set; } = false;
        [Parameter] public string? ListItemClass { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public bool PreventDefault { get; set; }
        [Parameter] public RenderFragment? TabContent { get; set; }
        [Parameter] public RenderFragment? TabLabel { get; set; }
        [Parameter] public string? Url { get; set; } = "javascript:void(0);";
        [CascadingParameter] public BSNav? Parent { get; set; }
        private bool _canHandleActive;
        private string? ClassBuilder => new CssBuilder("nav-link")
            .AddClass("active", IsActive ?? false)
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
            if (IsActive == null)
            {
                _canHandleActive = true;
                if (NavigationManager.Uri == NavigationManager.BaseUri + Url?.TrimStart('/'))
                    IsActive = true;
                if (NavigationManager.Uri.Contains(NavigationManager.BaseUri + Url?.TrimStart('/')) && ActiveOnChildRoutes)
                    IsActive = true;
                NavigationManager.LocationChanged += OnLocationChanged;
            }
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            if (!_canHandleActive) return;
            IsActive = false;
            if (NavigationManager.Uri == NavigationManager.BaseUri + Url?.TrimStart('/'))
                IsActive = true;
            if (NavigationManager.Uri.Contains(NavigationManager.BaseUri + Url?.TrimStart('/')) && ActiveOnChildRoutes)
                IsActive = true;
            StateHasChanged();
        }

        protected override void OnParametersSet()
        {
            if (Parent == null) return;
            if (Parent.IsTabs)
            {
                IsActive = Parent.SetFirstChild(this);
            }
            Parent.ChildHandler += Parent_ChildHandler;
        }

        private async Task ClickEvent()
        {
            if (!string.IsNullOrEmpty(Target))
                BlazorStrap.ForwardClick(Target);

            if (OnClick.HasDelegate)
                await OnClick.InvokeAsync();
            if (Parent?.IsTabs ?? false)
            {
                Parent.Invoke(this);

            }
        }

        private async void Parent_ChildHandler(BSNavItem sender)
        {
            if (sender == this) return;
            IsActive = false;
            await InvokeAsync(StateHasChanged);
        }
        public void Dispose()
        {
            if (_canHandleActive)
                NavigationManager.LocationChanged -= OnLocationChanged;
            if (Parent == null) return;
            if (Parent.ActiveChild == this)
                Parent.ActiveChild = null;
            Parent.ChildHandler -= Parent_ChildHandler;
        }
    }
}