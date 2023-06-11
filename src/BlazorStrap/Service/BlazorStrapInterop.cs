using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.RegularExpressions;

namespace BlazorStrap.Service
{
    public class BlazorStrapInterop
    {
        //Backdrop Events
        public Func<bool,Task>? SetRenderModalBackdrop { get; set; }
        public Func<Task>? OnModalBackdropShown { get; set; }
        public Func<bool, Task>? SetRenderOffCanvasBackdrop { get; set; }
        public Func<Task>? OnOffCanvasBackdropShown { get; set; }

        private IJSRuntime JsRuntime { get; }
        private IBlazorStrap BlazorStrap { get; }
        private IJSObjectReference? Module { get; set; }

        private DotNetObjectReference<BlazorStrapInterop>? _objectReference;
        public BlazorStrapInterop(IJSRuntime jsRuntime, IBlazorStrap blazorStrap)
        {
            BlazorStrap = blazorStrap;
            _objectReference = DotNetObjectReference.Create(this);
            JsRuntime = jsRuntime;
        }


        /// <summary>
        /// This method will add a document event to the document.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="elementReference"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        [Obsolete("Do not use will be removed in next version, Use IBlazorStrap.OnResize event instead")]
        public async ValueTask AddDocumentEventAsync(EventType eventType, string creatorId, bool ignoreChildren = false, CancellationToken? cancellationToken = null)
        {
            var eventName = Enum.GetName(typeof(EventType), eventType)?.ToLower() ?? "";
            
            var module = await GetModuleAsync();
            if (module is not null)
                await module.InvokeVoidAsync("addDocumentEvent", cancellationToken ?? CancellationToken.None, eventName, creatorId, _objectReference, ignoreChildren);
        }

        /// <summary>
        /// This method will remove a document event from the document.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="elementReference"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        [Obsolete("Do not use will be removed in next version, Use IBlazorStrap.OnResize event instead")]
        public async ValueTask RemoveDocumentEventAsync(EventType eventType, string creatorId, CancellationToken? cancellationToken = null)
        {
            var eventName = Enum.GetName(typeof(EventType), eventType)?.ToLower() ?? "";
            var module = await GetModuleAsync();
            if (module is not null)
                await module.InvokeVoidAsync("removeDocumentEvent", cancellationToken ?? CancellationToken.None, eventName, creatorId);
        }

        /// <summary>
        /// This method will add an event to the given targetId on behalf of the creator.
        /// </summary>
        /// <param name="targetId"></param>
        /// <param name="creator"></param>
        /// <param name="eventName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async ValueTask AddEventAsync(string targetId, string creatorId, EventType eventType, bool ignoreChildren = false, CancellationToken? cancellationToken = null)
        {
            var eventName = Enum.GetName(typeof(EventType), eventType)?.ToLower() ?? "";
            var module = await GetModuleAsync();
            if (module is not null)
                await module.InvokeVoidAsync("addEvent", cancellationToken ?? CancellationToken.None, targetId, creatorId, eventName, _objectReference, ignoreChildren);
        }

        /// <summary>
        /// This method will remove an event from the given targetId on behalf of the creator.
        /// </summary>
        /// <param name="targetId"></param>
        /// <param name="creator"></param>
        /// <param name="eventName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async ValueTask RemoveEventAsync(string targetId, string creatorId, EventType eventType, CancellationToken? cancellationToken = null)
        {
            var eventName = Enum.GetName(typeof(EventType), eventType)?.ToLower() ?? "";
            var module = await GetModuleAsync();
            if (module is not null)
                await module.InvokeVoidAsync("removeEvent", cancellationToken ?? CancellationToken.None, targetId, creatorId, eventName);
        }

        /// <summary>
        /// This method will show the tooltip and return a list of classes, styles, and ARIA attributes for the given element reference.
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="isPopper"></param>
        /// <param name="placement"></param>
        /// <param name="targetId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async ValueTask<InteropSyncResult?> ShowDropdownAsync(ElementReference elementReference, bool isPopper, string targetId, Placement placement, CancellationToken? cancellationToken = null, object? options = null)
        {
            var placementString = TranslatePlacementForPopperJs(placement);
            var module = await GetModuleAsync();
            return module is not null
                ? options is not null 
                    ? await module.InvokeAsync<InteropSyncResult?>("showDropdown", cancellationToken ?? CancellationToken.None, elementReference, isPopper, targetId, placementString, _objectReference, options) 
                    : await module.InvokeAsync<InteropSyncResult?>("showDropdown", cancellationToken ?? CancellationToken.None, elementReference, isPopper, targetId, placementString, _objectReference)
                : null;
        }

        /// <summary>
        /// This method will hide the tooltip and return a list of classes, styles, and ARIA attributes for the given element reference.
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async ValueTask<InteropSyncResult?> HideDropdownAsync(ElementReference elementReference, CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync();
            return module is not null
                ? await module.InvokeAsync<InteropSyncResult?>("hideDropdown", cancellationToken ?? CancellationToken.None, elementReference, _objectReference)
                : null;
        }

        /// <summary>
        /// This method will show the tooltip and return a list of classes, styles, and ARIA attributes for the given element reference.
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="placement"></param>
        /// <param name="targetId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async ValueTask<InteropSyncResult?> ShowTooltipAsync(ElementReference elementReference, Placement placement, string targetId, CancellationToken? cancellationToken = null, object? options = null)
        {
            var placementString = TranslatePlacementForPopperJs(placement);
            var module = await GetModuleAsync();
            return module is not null
                ? options is not null 
                    ? await module.InvokeAsync<InteropSyncResult?>("showTooltip", cancellationToken ?? CancellationToken.None, elementReference, placementString, targetId, _objectReference, options) 
                    : await module.InvokeAsync<InteropSyncResult?>("showTooltip", cancellationToken ?? CancellationToken.None, elementReference, placementString, targetId, _objectReference)
                : null;
        }

        /// <summary>
        /// This method will hide the tooltip and return a list of classes, styles, and ARIA attributes for the given element reference.
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async ValueTask<InteropSyncResult?> HideTooltipAsync(ElementReference elementReference, CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync();
            return module is not null
                ? await module.InvokeAsync<InteropSyncResult?>("hideTooltip", cancellationToken ?? CancellationToken.None, elementReference, _objectReference)
                : null;
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
            var module = await GetModuleAsync();
            return module is not null
                ? await module.InvokeAsync<InteropSyncResult>("showCollapse", cancellationToken ?? CancellationToken.None, elementReference, IsHorizontal, _objectReference)
                : null;
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
            var module = await GetModuleAsync();
            return module is not null
                ? await module.InvokeAsync<InteropSyncResult>("hideCollapse", cancellationToken ?? CancellationToken.None, elementReference, IsHorizontal, _objectReference)
                : null;
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
            var module = await GetModuleAsync();
            return module is not null
                ? await module.InvokeAsync<InteropSyncResult>("showModal", cancellationToken ?? CancellationToken.None, elementReference, _objectReference)
                : null;
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
            var module = await GetModuleAsync();
            return module is not null
                ? await module.InvokeAsync<InteropSyncResult>("hideModal", cancellationToken ?? CancellationToken.None, elementReference, _objectReference)
                : null;
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
            var module = await GetModuleAsync();
            return module is not null
                ? await module.InvokeAsync<InteropSyncResult>("showOffcanvas", cancellationToken ?? CancellationToken.None, elementReference, _objectReference)
                : null;
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
            var module = await GetModuleAsync();
            return module is not null
                ? await module.InvokeAsync<InteropSyncResult>("hideOffcanvas", cancellationToken ?? CancellationToken.None, elementReference, _objectReference)
                : null;
        }

        /// <summary>
        /// This method will show the active accordion and hide the old one. Then return a list of classes, styles, and ARIA attributes for the given element reference.
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="closeElement"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async ValueTask<List<InteropSyncResult?>> ShowAccordionAsync(ElementReference elementReference, ElementReference? closeElement, CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync();
            return module is not null
                ? await module.InvokeAsync<List<InteropSyncResult?>>("showAccordion", cancellationToken ?? CancellationToken.None, elementReference, closeElement, _objectReference)
                : new();
        }

       

        /// <summary>
        /// Adds the toast timer to ElementReference
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="time"></param>
        /// <param name="timeRemaining"></param>
        /// <param name="rendered"></param>
        /// <param name="cancellationToken"></param>
        public async ValueTask ToastTimerAsync(ElementReference? elementReference, int time, int timeRemaining, bool rendered, CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync();
            if (elementReference is null || module is null) return;
                await module.InvokeVoidAsync("toastTimer", cancellationToken ?? CancellationToken.None, elementReference, time, timeRemaining, rendered);
        }
        //TODO: Direct Points from old JS
        /// <summary>
        /// Triggers Carousel to animate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="showElementReference"></param>
        /// <param name="hideElementReference"></param>
        /// <param name="back"></param>
        /// <param name="cancellationToken"></param>
        /// /// <returns>bool</returns>
        public async ValueTask<bool> AnimateCarouselAsync(string id, ElementReference? showElementReference, ElementReference? hideElementReference, bool back, bool v4, CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync();
            if (module is null)
                throw new NullReferenceException("Unable to load module.");
            if (showElementReference == null)
                throw new ArgumentNullException(nameof(showElementReference));
            if (hideElementReference == null)
                throw new ArgumentNullException(nameof(hideElementReference));

            return await module.InvokeAsync<bool>("animateCarousel", cancellationToken ?? CancellationToken.None, id, showElementReference, hideElementReference, back, v4, _objectReference);
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

        /// <summary>
        /// Sanitity checks javascritps eventCallback array
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async ValueTask RemoveRougeEventsAsync(CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync();
            if (module is not null)
                await module.InvokeVoidAsync("removeRougeEvents", cancellationToken ?? CancellationToken.None);
        }
        /// <summary>
        /// Sanitity check for the backdrop.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async ValueTask CheckBackdropsAsync(CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync();
            if (module is not null)
                await module.InvokeVoidAsync("checkBackdrops", cancellationToken ?? CancellationToken.None, _objectReference);
        }

        //////////////////////
        // Utility Methods //
        ////////////////////
        public async ValueTask AddBodyClassAsync(string className, CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync();
            if (module is not null)
                await module.InvokeVoidAsync("addBodyClass", cancellationToken ?? CancellationToken.None, className);
        }

        public async ValueTask RemoveBodyClassAsync(string className, CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync();
            if (module is not null)
                await module.InvokeVoidAsync("removeBodyClass", cancellationToken ?? CancellationToken.None, className);
        }
        public async ValueTask SetBodyStyleAsync(string style, string value, CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync();
            if (module is not null)
                await module.InvokeVoidAsync("setBodyStyle", cancellationToken ?? CancellationToken.None, style, value);
        }

        public async ValueTask AddClassAsync(ElementReference elementReference, string className, int delay = 0, CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync();
            if (module is not null)
                await module.InvokeVoidAsync("addClass", cancellationToken ?? CancellationToken.None, elementReference, className, delay);
        }

        public async ValueTask RemoveClassAsync(ElementReference elementReference, string className, int delay = 0, CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync();
            if (module is not null)
                await module.InvokeVoidAsync("removeClass", cancellationToken ?? CancellationToken.None, elementReference, className, delay);
        }
        public async ValueTask SetStyleAsync(ElementReference elementReference, string style, string value, int delay = 0 , CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync();
            if (module is not null)
                await module.InvokeVoidAsync("setStyle", cancellationToken ?? CancellationToken.None, elementReference, style, value, delay);
        }
        public async ValueTask BlurAllAsync(CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync();
            if (module is not null)
                await module.InvokeVoidAsync("blurAll", cancellationToken ?? CancellationToken.None);
        }

        public async ValueTask AddAttributeAsync(ElementReference elementReference, string name, string value, CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync();
            if (module is not null)
                await module.InvokeVoidAsync("addAttribute", cancellationToken ?? CancellationToken.None, elementReference, name, value);
        }

        public async ValueTask RemoveAttributeAsync(ElementReference elementReference, string name, CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync();
            if (module is not null)
                await module.InvokeVoidAsync("removeAttribute", cancellationToken ?? CancellationToken.None, elementReference, name);
        }
       
        public async ValueTask<int?> GetHeightAsync(ElementReference elementReference, CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync();
            return module is not null ? await module.InvokeAsync<int?>("getHeight", CancellationToken.None, elementReference) : null;
        }
        public async ValueTask<int?> GetWidthAsync(ElementReference elementReference, CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync();
            return module is not null ? await module.InvokeAsync<int?>("getWidth", CancellationToken.None, elementReference) : null;
        }
        public async ValueTask<bool> SetBootstrapCssAsync(string? themeUrl, CancellationToken? cancellationToken = null)
        {
            var module = await GetModuleAsync();
            return module is not null && await module.InvokeAsync<bool>("setBootstrapCss", cancellationToken ?? CancellationToken.None, themeUrl);
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
            if(sender == "jsdocument" && type == EventType.Resize && data is int width)
                await BlazorStrap.InvokeResize(width);
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
        private static string TranslatePlacementForPopperJs(Placement value)
        {
            var result = Enum.GetName(typeof(Placement), value);
            //Add dash between Pasle cased workds example BottomLeft to Bottom-Left
            if (result is not null)
                result = Regex.Replace(result, "([a-z])([A-Z])", "$1-$2");
            return result?.ToLower() ?? "";
        }
    }
}
   