using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Reflection;
using System.Threading.Tasks;

namespace BlazorStrap.Service
{
    public class Interop
    {
        public IJSRuntime JsRuntime { get; }
        public IJSObjectReference? Module;
        public Interop(IJSRuntime jsRuntime)
        {
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
             return await module.InvokeAsync<InteropSyncResult>("showCollapse", cancellationToken ?? CancellationToken.None, elementReference, IsHorizontal);
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
            return await module.InvokeAsync<InteropSyncResult>("hideCollapse", cancellationToken ?? CancellationToken.None, elementReference, IsHorizontal);
        }

        /// <summary>
        /// This method will show the modal and return a list of classes, styles, and ARIA attributes for the given element reference.
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async ValueTask<InteropSyncResult?> ShowModalAsync(ElementReference elementReference, CancellationToken? cancellationToken = null)
        {
            
            var module = await GetModuleAsync() ?? throw new NullReferenceException("Unable to load module.");
            return await module.InvokeAsync<InteropSyncResult>("showModal", cancellationToken ?? CancellationToken.None, elementReference);
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
            return await module.InvokeAsync<InteropSyncResult>("hideModal", cancellationToken ?? CancellationToken.None, elementReference);
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
   