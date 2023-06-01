﻿using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components
{
    public class BSCoreBase : ComponentBase, IDisposable
    {
        [Inject] protected IBlazorStrap? BlazorStrapService { get; set; } = default!;
        [Parameter] public bool HasToaster { get; set; } = true;

        protected bool _modalBackdropRendered;
        protected bool _offcanvasBackdropRendered;
        protected bool _backdropShown;
        protected bool _offcanvasBackdropShown;
        protected override void OnInitialized()
        {
            if (BlazorStrapService is not null)
            {
                BlazorStrapService.JavaScriptInterop.OnOffCanvasBackdropShown += OnOffCanvasBackdropShown;
                BlazorStrapService.JavaScriptInterop.SetRenderOffCanvasBackdrop += SetRenderOffcanvasBackdrop;
                BlazorStrapService.JavaScriptInterop.SetRenderModalBackdrop += SetRenderModalBackdrop;
                BlazorStrapService.JavaScriptInterop.OnModalBackdropShown += OnModalBackdropShown;
            }
        }

        private async Task SetRenderOffcanvasBackdrop(bool value)
        {
            _offcanvasBackdropRendered = value;
            if (_offcanvasBackdropShown && !value)
            {
                _offcanvasBackdropShown = false;
            }
            await InvokeAsync(StateHasChanged);
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
            _modalBackdropRendered = value;
            if(_backdropShown && !value)
            {
                  _backdropShown = false;
            }
            
            await InvokeAsync(StateHasChanged);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (BlazorStrapService is null) throw new ArgumentNullException(nameof(BlazorStrapService));
                await BlazorStrapService.JavaScriptInterop.PreloadModuleAsync();
            }
        }

        public void Dispose()
        {
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
