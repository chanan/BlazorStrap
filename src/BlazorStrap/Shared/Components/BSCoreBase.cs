using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace BlazorStrap.Shared.Components
{
    public class BSCoreBase : ComponentBase, IDisposable
    {
        [Inject] protected IBlazorStrap? BlazorStrapService { get; set; } = default!;
        [Inject] NavigationManager? NavigationManager { get; set; }
        [Parameter] public bool HasToaster { get; set; } = true;

        protected bool _modalBackdropRendered;
        protected bool _offcanvasBackdropRendered;
        protected bool _backdropShown;
        protected bool _offcanvasBackdropShown;
        private bool _secondRender;
        private bool _locationChanged;
        private TaskCompletionSource<bool> TaskCompletionSource { get; set; } = new TaskCompletionSource<bool>();
        protected override void OnInitialized()
        {
            if(NavigationManager is not null)
            {
                NavigationManager.LocationChanged += OnLocationChanged;
            }
            if (BlazorStrapService is not null)
            {
                BlazorStrapService.JavaScriptInterop.OnOffCanvasBackdropShown += OnOffCanvasBackdropShown;
                BlazorStrapService.JavaScriptInterop.SetRenderOffCanvasBackdrop += SetRenderOffcanvasBackdrop;
                BlazorStrapService.JavaScriptInterop.SetRenderModalBackdrop += SetRenderModalBackdrop;
                BlazorStrapService.JavaScriptInterop.OnModalBackdropShown += OnModalBackdropShown;
            }
        }

        private async void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            _locationChanged = true;
            await InvokeAsync(StateHasChanged);
        }

        private async Task SetRenderOffcanvasBackdrop(bool value)
        {
            TaskCompletionSource = new TaskCompletionSource<bool>();
            _offcanvasBackdropRendered = value;
            if (_offcanvasBackdropShown && !value)
            {
                _offcanvasBackdropShown = false;
            }
            await InvokeAsync(StateHasChanged);
            await TaskCompletionSource.Task;
        }

        private async Task OnOffCanvasBackdropShown()
        {
            _offcanvasBackdropShown = true;
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnModalBackdropShown()
        {
            _backdropShown = true;
            await InvokeAsync(StateHasChanged);
        }

        // This is only called when no other backdrops are shown so we can safely set the backdrop to false when the last modal with a backdrop is closed.
        private async Task SetRenderModalBackdrop(bool value)
        {
            TaskCompletionSource = new TaskCompletionSource<bool>();
            _modalBackdropRendered = value;
            if(_backdropShown && !value)
            {
                  _backdropShown = false;
            }
            
            await InvokeAsync(StateHasChanged);
            await TaskCompletionSource.Task;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _secondRender = true;
                if (BlazorStrapService is null) throw new ArgumentNullException(nameof(BlazorStrapService));
                await BlazorStrapService.JavaScriptInterop.PreloadModuleAsync();
            }
            else if(!firstRender && _secondRender)
            {
                TaskCompletionSource.TrySetResult(true);
            }
            if (_locationChanged && _secondRender)
            {
                _locationChanged = false;
                if(BlazorStrapService is not null)
                    await BlazorStrapService.JavaScriptInterop.RemoveRougeEventsAsync();
            }
        }

        public void Dispose()
        {
            if (NavigationManager is not null)
            {
                NavigationManager.LocationChanged -= OnLocationChanged;
            }
            if (BlazorStrapService is not null)
            {
                BlazorStrapService.JavaScriptInterop.OnOffCanvasBackdropShown -= OnOffCanvasBackdropShown;
                BlazorStrapService.JavaScriptInterop.SetRenderOffCanvasBackdrop -= SetRenderOffcanvasBackdrop;
                BlazorStrapService.JavaScriptInterop.SetRenderModalBackdrop -= SetRenderModalBackdrop;
                BlazorStrapService.JavaScriptInterop.OnModalBackdropShown -= OnModalBackdropShown;
            }
        }
    }
}
