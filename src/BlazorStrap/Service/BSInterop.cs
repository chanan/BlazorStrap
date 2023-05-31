using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap.Service
{
    public class BSInterop
    {
        public IJSRuntime JsRuntime { get; }
        public IJSObjectReference? Module;
        public Func<bool,Task>? SetRenderModalBackdrop { get; set; }
        public Func<Task>? OnModalBackdropShown { get; set; }
        private DotNetObjectReference<BSInterop>? _objectReference;
        public BSInterop(IJSRuntime jsRuntime)
        {
            _objectReference = DotNetObjectReference.Create(this);
            JsRuntime = jsRuntime;
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
        public async Task RequestBackdropAsync(bool value)
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
   