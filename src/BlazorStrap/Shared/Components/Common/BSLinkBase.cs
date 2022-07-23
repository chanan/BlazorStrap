using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSLinkBase<TSize> : BlazorStrapActionBase<TSize> where TSize : Enum
    {
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;

        /// <summary>
        /// Sets button as reset type.
        /// </summary>
        [Parameter]
        public bool IsReset
        {
            get => IsResetType;
            set => IsResetType = value;
        }

        /// <summary>
        /// Sets button as submit type.
        /// </summary>
        [Parameter]
        public bool IsSubmit
        {
            get => IsSubmitType;
            set => IsSubmitType = value;
        }

        /// <summary>
        /// Renders link as a button.
        /// </summary>
        [Parameter]
        public bool IsButton
        {
            get => HasButtonClass;
            set => HasButtonClass = value;
        }

        /// <summary>
        /// Url to link to.
        /// </summary>
        [Parameter]
        public string? Url
        {
            get => UrlBase;
            set => UrlBase = value;
        }

        public BSLinkBase()
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
            if (_canHandleActive)
                NavigationManager.LocationChanged -= OnLocationChanged;
        }
    }
}
