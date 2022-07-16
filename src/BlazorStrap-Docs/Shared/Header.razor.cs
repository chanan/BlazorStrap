using Microsoft.AspNetCore.Components;
using BlazorStrap;
using BlazorStrap_Docs.Service;
using BlazorStrap.V5;
namespace BlazorStrap_Docs.Shared
{
    public partial class Header : IDisposable
    {
        [Inject] private NavigationManager _navigationManager { get; set; }
        [Inject] private Core _core { get; set; }
        [Inject] private IBlazorStrap _blazorStrap { get; set; }
        [CascadingParameter]
        public MainLayout Parent { get; set; } = null!;

        private string _version = "5.1.3";
        private string _sidebarButtonClass = "bd-sidebar-toggle d-md-none collapsed";
        private string Selected { get; set; } = "Bootstrap";

        private List<string> _themes = new List<string>();
        protected override void OnInitialized()
        {
            _core.OnVersionChanged += _core_OnVersionChanged;

            if (Parent != null)
                Parent.OnSideBarShown += OnSideBarShown;
        }

        private async void _core_OnVersionChanged(string version)
        {
            Selected = "Bootstrap"; 
            if (version == "V4")
            {
                _themes = Enum.GetNames(typeof(BlazorStrap.V4.Theme)).ToList();
            }
            else
            {
                _themes = Enum.GetNames(typeof(BlazorStrap.V5.Theme)).ToList();
            }
            _version = Settings.Versions[version];
            await _blazorStrap.SetBootstrapCss(_version);
            StateHasChanged();
        }

        protected override async Task OnAfterRenderAsync(bool firstrun)
        {
            if (firstrun)
            {
                await _blazorStrap.SetBootstrapCss(_version);
            }
        }
        private void VersionChanged(string version)
        {
            _navigationManager.NavigateTo($"/{version}");
        }
        private async Task SelectedChanged(string value)
        {
            Selected = value;
            await _blazorStrap.SetBootstrapCss(value, _version);
        }
        private async Task OnSideBarShown(bool state)
        {
            if (state)
                _sidebarButtonClass = "bd-sidebar-toggle d-md-none";
            else
                _sidebarButtonClass = "bd-sidebar-toggle d-md-none collapsed";
            await InvokeAsync(StateHasChanged);
        }
        public void Dispose()
        {
            _core.OnVersionChanged -= _core_OnVersionChanged;
            if (Parent != null)
                Parent.OnSideBarShown -= OnSideBarShown;
        }
    }
}