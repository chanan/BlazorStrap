//using Microsoft.AspNetCore.Components;
//using BlazorStrap;

//namespace BlazorStrap_Docs.Shared
//{
//    public partial class Header
//    {
//        [CascadingParameter]
//        public MainLayout Parent { get; set; } = null!;
//        private string _version = "5.1.3";
//        private string _sidebarButtonClass = "bd-sidebar-toggle d-md-none collapsed";
//        private string Selected { get; set; } = "Bootstrap";

//        private List<string> _themes = new List<string>();
//        protected override void OnInitialized()
//        {
//            _themes = Enum.GetNames(typeof(Theme)).ToList();
//            if (Parent != null)
//                Parent.OnSideBarShown += OnSideBarShown;
//        }

//        protected override async Task OnAfterRenderAsync(bool firstrun)
//        {
//            if (firstrun)
//            {
//                await _blazorStrap.SetBootstrapCss(_version);
//            }
//        }
//        private async Task VersionChanged(string version)
//        {
//            _version = version;
//            if(_version == "4.6.1")
//            {
//                _themes = Enum.GetNames(typeof(Bootstrap4Themes)).ToList();
//            }
//            else
//            {
//                _themes = Enum.GetNames(typeof(Theme)).ToList();
//            }
//            Selected = "Bootstrap";
//            await _blazorStrap.SetBootstrapCss(_version);
//            await Parent.Refresh();
//        }
//        private async Task SelectedChanged(string value)
//        {
//            Selected = value;
//            await _blazorStrap.SetBootstrapCss(value, _version);
//        }
//        private async Task OnSideBarShown(bool state)
//        {
//            if(state)
//                _sidebarButtonClass = "bd-sidebar-toggle d-md-none";
//            else
//                _sidebarButtonClass = "bd-sidebar-toggle d-md-none collapsed";
//            await InvokeAsync(StateHasChanged);
//        }
//        public void Dispose()
//        {
//            if (Parent != null)
//                Parent.OnSideBarShown -= OnSideBarShown;
//        }
//    }
//}