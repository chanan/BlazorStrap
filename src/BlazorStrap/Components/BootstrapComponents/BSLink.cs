using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public class BSLink : BlazorStrapActionBase
    {
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Parameter] public bool IsReset {
            get => IsResetType;
            set => IsResetType = value;
        }
        [Parameter] public bool IsSubmit{
            get => IsSubmitType;
            set => IsSubmitType = value;
        }
        [Parameter] public bool IsButton{
            get => HasButtonClass;
            set => HasButtonClass = value;
        }
        [Parameter] public string? Url{
            get => UrlBase;
            set => UrlBase = value;
        }
        public BSLink()
        {
            IsLinkType = true;
        }
        private bool _canHandleActive;
        protected override void OnInitialized()
        {
            if (IsActive == null)
            {
                _canHandleActive = true;
                if (NavigationManager.Uri == NavigationManager.BaseUri + Url?.TrimStart('/'))
                {
                    IsActive = true;
                }
                NavigationManager.LocationChanged += OnLocationChanged;
            }
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            if (!_canHandleActive) return;
            IsActive = false;
            if (NavigationManager.Uri == NavigationManager.BaseUri + Url?.TrimStart('/'))
            {
                IsActive = true;
            }
            StateHasChanged();
        }
        
        public void Dispose()
        {
            if( _canHandleActive)
                NavigationManager.LocationChanged -= OnLocationChanged;
        }
    }
}
