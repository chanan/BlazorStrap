using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap.Service
{
    public class BSInterop
    {
        //Backdrop Events
        public Func<bool,Task>? SetRenderModalBackdrop { get; set; }
        public Func<Task>? OnModalBackdropShown { get; set; }
        public Func<bool, Task>? SetRenderOffCanvasBackdrop { get; set; }
        public Func<Task>? OnOffCanvasBackdropShown { get; set; }

        private IJSRuntime JsRuntime { get; }
        private IBlazorStrap BlazorStrap { get; }
        private IJSObjectReference? Module { get; set; }

        private DotNetObjectReference<BSInterop>? _objectReference;
        public BSInterop(IJSRuntime jsRuntime, IBlazorStrap blazorStrap)
        {
            _objectReference = DotNetObjectReference.Create(this);
            JsRuntime = jsRuntime;
            BlazorStrap = blazorStrap;
        }

        /// <summary>
        /// This method will show the collapse and return a list of classes, styles, and ARIA attributes for the given element reference.
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async ValueTask<InteropSyncResult?> ShowCollapseAsync(ElementReference elementReference, bool IsHorizontal = false,  CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync() ?? throw new NullReferenceException("Unable to load module.");
             return await module.InvokeAsync<InteropSyncResult>("showCollapse", cancellationToken ?? CancellationToken.None, elementReference, IsHorizontal, _objectReference);
        }
        /// <summary>
        /// This method will hide the collapse and return a list of classes, styles, and ARIA attributes for the given element reference.
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async ValueTask<InteropSyncResult?> HideCollapseAsync(ElementReference elementReference, bool IsHorizontal = false, CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync() ?? throw new NullReferenceException("Unable to load module.");
            return await module.InvokeAsync<InteropSyncResult>("hideCollapse", cancellationToken ?? CancellationToken.None, elementReference, IsHorizontal, _objectReference);
        }

        /// <summary>
        /// This method will show the modal and return a list of classes, styles, and ARIA attributes for the given element reference.
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async ValueTask<InteropSyncResult?> ShowModalAsync(ElementReference elementReference, bool showBackdrop, CancellationToken? cancellationToken = null)
        {
            if(showBackdrop)
                await RequestBackdropAsync(true);
            var module = await GetModuleAsync() ?? throw new NullReferenceException("Unable to load module.");
            return await module.InvokeAsync<InteropSyncResult>("showModal", cancellationToken ?? CancellationToken.None, elementReference, _objectReference);
        }
        /// <summary>
        /// This method will hide the modal and return a list of classes, styles, and ARIA attributes for the given element reference.
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async ValueTask<InteropSyncResult?> HideModalAsync(ElementReference elementReference, CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync() ?? throw new NullReferenceException("Unable to load module.");
            return await module.InvokeAsync<InteropSyncResult>("hideModal", cancellationToken ?? CancellationToken.None, elementReference, _objectReference);
        }

        /// <summary>
        /// This method will show the offcanvas and return a list of classes, styles, and ARIA attributes for the given element reference.
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="showBackdrop"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async ValueTask<InteropSyncResult?> ShowOffCanvasAsync(ElementReference elementReference, bool showBackdrop, CancellationToken? cancellationToken = null)
        {
            if (showBackdrop)
                await RequestOffCanvasBackdropAsync(true);
            var module = await GetModuleAsync() ?? throw new NullReferenceException("Unable to load module.");
            return await module.InvokeAsync<InteropSyncResult>("showOffcanvas", cancellationToken ?? CancellationToken.None, elementReference, _objectReference);
        }

        /// <summary>
        /// This method will hide the offcanvas and return a list of classes, styles, and ARIA attributes for the given element reference.
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async ValueTask<InteropSyncResult?> HideOffCanvasAsync(ElementReference elementReference, CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync() ?? throw new NullReferenceException("Unable to load module.");
            return await module.InvokeAsync<InteropSyncResult>("hideOffcanvas", cancellationToken ?? CancellationToken.None, elementReference, _objectReference);
        }

        /// <summary>
        /// Sanitity check for the backdrop.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async ValueTask CheckBackdropsAsync(CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync() ?? throw new NullReferenceException("Unable to load module.");
            await module.InvokeVoidAsync("checkBackdrops", cancellationToken ?? CancellationToken.None, _objectReference);
        }
        /// <summary>
        /// This method preloads the module and sets the module reference.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async ValueTask PreloadModuleAsync(CancellationToken? cancellationToken = null)
        {
            _ = await GetModuleAsync() ?? throw new NullReferenceException("Unable to load module.");
        }

        [JSInvokable]
        public async Task RemoveBackdropAsync()
        {
            if (SetRenderModalBackdrop is null) return;
            await SetRenderModalBackdrop.Invoke(false);
        }

        [JSInvokable]
        public async Task RemoveOffCanvasBackdropAsync()
        {
            if (SetRenderOffCanvasBackdrop is null) return;
            await SetRenderOffCanvasBackdrop.Invoke(false);
        }
        [JSInvokable]
        public async Task BackdropShownAsync()
        {
            if (SetRenderModalBackdrop is null) return;
            await SetRenderModalBackdrop.Invoke(true);
        }
        [JSInvokable]
        public async Task OffCanvasBackdropShownAsync()
        {
            if (SetRenderOffCanvasBackdrop is null) return;
            await SetRenderOffCanvasBackdrop.Invoke(true);
        }
        [JSInvokable]
        public async Task InvokeEventAsync(string sender, string target, EventType type, object data)
        {
            await BlazorStrap.InvokeEvent(sender, target, type, data);   
        }
        private async Task RequestBackdropAsync(bool value)
        {
            if (SetRenderModalBackdrop is null) return;
            var instances = SetRenderModalBackdrop.GetInvocationList();
            
            var tasks = new List<Task>();
            foreach (var instance in instances)
            {
                if(instance is Func<bool, Task> func)
                    tasks.Add(func.Invoke(value));
            }
            await Task.WhenAll(tasks);
        }

        private async Task RequestOffCanvasBackdropAsync(bool value)
        {
            if (SetRenderOffCanvasBackdrop is null) return;
            var instances = SetRenderOffCanvasBackdrop.GetInvocationList();

            var tasks = new List<Task>();
            foreach (var instance in instances)
            {
                if (instance is Func<bool, Task> func)
                    tasks.Add(func.Invoke(value));
            }
            await Task.WhenAll(tasks);
        }

        private async Task<IJSObjectReference?> GetModuleAsync()
        {
            if (Module is not null)
            {
                return Module;
            }
            try
            {
                Module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorStrap/blazorstrapinterop.js");
                return Module;
            }
            catch
            {
                return null;
            }
        }
    }
}
   