﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap.Service
{
    public class BlazorStrapInterop : IDisposable
    {
        private readonly IJSRuntime _jsRuntime; 
        private readonly IJSInProcessRuntime? _jSInProcessRuntime;
        private bool _disposedValue;
        public BlazorStrapInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _jSInProcessRuntime = jsRuntime as IJSInProcessRuntime;
        }
        /// <summary>
        /// Adds Attribute to ElementReference
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="cancellationToken"></param>
        public ValueTask AddAttributeAsync(ElementReference elementReference, string name, string value, CancellationToken? cancellationToken = null) 
            => _jsRuntime.InvokeVoidAsync("blazorStrap.AddAttribute", cancellationToken ?? CancellationToken.None ,elementReference, name, value);
        
        /// <summary>
        /// Adds a class to the body.
        /// </summary>
        /// <param name="className"></param>
        /// <param name="cancellationToken"></param>
        public ValueTask AddBodyClassAsync(string className, CancellationToken? cancellationToken = null) 
            => _jsRuntime.InvokeVoidAsync("blazorStrap.AddBodyClass", cancellationToken ?? CancellationToken.None ,className);
        
        /// <summary>
        /// Adds a class to the ElementReference
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="className"></param>
        /// <param name="cancellationToken"></param>

        public ValueTask AddClassAsync(ElementReference elementReference, string className, CancellationToken? cancellationToken = null) 
            => _jsRuntime.InvokeVoidAsync("blazorStrap.AddClass", cancellationToken ?? CancellationToken.None, elementReference, className, 0);
        
        /// <summary>
        /// Adds a class to the ElementReference with a forced return delay.
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="className"></param>
        /// <param name="delay"></param>
        /// <param name="cancellationToken"></param>
        public ValueTask AddClass(ElementReference elementReference, string className, int delay = 0, CancellationToken? cancellationToken = null) 
            => _jsRuntime.InvokeVoidAsync("blazorStrap.AddClass", cancellationToken ?? CancellationToken.None, elementReference, className, delay);
        
        // Not correct in js yet
        /// <summary>
        /// Adds event to ElementReference.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="cancellationToken"></param>
        public ValueTask AddEvent(string id, string name, EventTypes type, CancellationToken? cancellationToken = null) 
            => _jsRuntime.InvokeVoidAsync("blazorStrap.AddEvent", cancellationToken ?? CancellationToken.None, id, name, type.NameToLower());
        
        /// <summary>
        /// Adds event to ElementReference.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="ignoreChildren"></param>
        /// <param name="cancellationToken"></param>
        public ValueTask AddEvent(string id, string name, EventTypes type, bool ignoreChildren, CancellationToken? cancellationToken = null) 
            => _jsRuntime.InvokeVoidAsync("blazorStrap.AddEvent", cancellationToken ?? CancellationToken.None, id, name, type.NameToLower(), ignoreChildren);
        
        /// <summary>
        /// Adds event to ElementReference
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="classFilter">Likely to be removed.</param>
        /// <param name="cancellationToken"></param>
        public ValueTask AddEvent(string id, string name, EventTypes type, string classFilter, CancellationToken? cancellationToken = null) 
            => _jsRuntime.InvokeVoidAsync("blazorStrap.AddEvent", cancellationToken ?? CancellationToken.None, id, name, type.NameToLower(), false, classFilter);
        
        /// <summary>
        /// Adds event to ElementReference
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="ignoreChildren"></param>
        /// <param name="classFilter">Likely to be removed.</param>
        /// <param name="cancellationToken"></param>
        public ValueTask AddEventAsync(string id, string name, EventTypes type, bool ignoreChildren = false, string classFilter = "", CancellationToken? cancellationToken = null) 
            => _jsRuntime.InvokeVoidAsync("blazorStrap.AddEvent", cancellationToken ?? CancellationToken.None, id, name, type.NameToLower(), ignoreChildren, classFilter);
        
        // Not in js yet
        public ValueTask AddDocumentEventAsync(string id, string name, EventTypes type, bool ignoreChildren = false, string classFilter = "", CancellationToken? cancellationToken = null) 
            => _jsRuntime.InvokeVoidAsync("blazorStrap.AddDocumentEvent", cancellationToken ?? CancellationToken.None, id, name, type.NameToLower(), ignoreChildren,classFilter);
        
        /// <summary>
        /// Adds a Popover to ElementReference
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="placement"></param>
        /// <param name="target"></param>
        /// <param name="cancellationToken"></param>
        public ValueTask AddPopoverAsync(ElementReference elementReference, Placement placement, string target, CancellationToken? cancellationToken = null)
            => _jsRuntime.InvokeVoidAsync("blazorStrap.AddPopover", cancellationToken ?? CancellationToken.None, elementReference, placement.Name().ToDashSeperated(), target );
        
        /// <summary>
        /// Adds a Popover to ElementReference
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="placement"></param>
        /// <param name="target"></param>
        /// <param name="offset"></param>
        /// <param name="cancellationToken"></param>
        public ValueTask AddPopoverAsync(ElementReference elementReference, Placement placement, string target, string offset = "none",  CancellationToken? cancellationToken = null)
            => _jsRuntime.InvokeVoidAsync("blazorStrap.AddPopover", cancellationToken ?? CancellationToken.None, elementReference, placement.Name().ToDashSeperated(), target, offset );

        /// <summary>
        /// Triggers Carousel to animate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="showElementReference"></param>
        /// <param name="hideElementReference"></param>
        /// <param name="back"></param>
        /// <param name="cancellationToken"></param>
        public ValueTask AnimateCarouselAsync(string id, ElementReference showElementReference, ElementReference hideElementReference, bool back, CancellationToken? cancellationToken = null)
            => _jsRuntime.InvokeVoidAsync("blazorStrap.AnimateCarousel", cancellationToken ?? CancellationToken.None, id, showElementReference, hideElementReference, back);
        
        /// <summary>
        /// Triggers Collapse to animate
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="id"></param>
        /// <param name="shown"></param>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        public ValueTask AnimateCollapseAsync(ElementReference elementReference, string id, bool shown, string name, CancellationToken? cancellationToken = null)
            => _jsRuntime.InvokeVoidAsync("blazorStrap.AnimateCarousel", cancellationToken ?? CancellationToken.None, elementReference, id, shown,name);

        /// <summary>
        /// Returns a string array of all child data-blazorstrap ids
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>string[]</returns>
        public ValueTask<string[]> GetChildrenIdsAsync(ElementReference elementReference, CancellationToken? cancellationToken = null)
         => _jsRuntime.InvokeAsync<string[]>("blazorStrap.GetChildrenIds", CancellationToken.None, elementReference);
        
        /// <summary>
        /// Returns the height of the ElementReference
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>int</returns>
        public ValueTask<int> GetHeightAsync(ElementReference elementReference, CancellationToken? cancellationToken = null)
            => _jsRuntime.InvokeAsync<int>("blazorStrap.GetHeight", CancellationToken.None, elementReference);
        
        /// <summary>
        /// Returns the windows inner height
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>int</returns>
        public ValueTask<int> GetWindowInnerHeightAsync(CancellationToken? cancellationToken = null)
            => _jsRuntime.InvokeAsync<int>("blazorStrap.GetWindowInnerHeight", CancellationToken.None);
        
        /// <summary>
        /// Gets the body's scrollbar width
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>int</returns>
        public ValueTask<int> GetScrollBarWidth(CancellationToken? cancellationToken = null)
            => _jsRuntime.InvokeAsync<int>("blazorStrap.GetScrollBarWidth", CancellationToken.None);
        
        /// <summary>
        /// returns the ElementReference style list
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>string</returns>
        public ValueTask<string> GetStyleAsync(ElementReference elementReference, CancellationToken? cancellationToken = null)
            => _jsRuntime.InvokeAsync<string>("blazorStrap.GetStyle", CancellationToken.None, elementReference);
        
        /// <summary>
        /// returns the ElementReference height. By setting display block, visibility hidden, position absolute for a moment.
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>int</returns>
        public ValueTask<int> PeakHeightAsync(ElementReference elementReference, CancellationToken? cancellationToken = null)
            => _jsRuntime.InvokeAsync<int>("blazorStrap.PeakHeight", CancellationToken.None, elementReference);
        
        /// <summary>
        /// Removes Attribute from the ElementReference
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        public ValueTask RemoveAttributeAsync(ElementReference elementReference, string name, CancellationToken? cancellationToken = null)
            => _jsRuntime.InvokeVoidAsync("blazorStrap.RemoveAttribute", CancellationToken.None, elementReference, name);
        
        /// <summary>
        /// Remove a class from the body
        /// </summary>
        /// <param name="className"></param>
        /// <param name="cancellationToken"></param>
        public ValueTask RemoveBodyClassAsync(string className, CancellationToken? cancellationToken = null)
            => _jsRuntime.InvokeVoidAsync("blazorStrap.RemoveBodyClass", CancellationToken.None, className);
        
        /// <summary>
        /// Removes a class from the ElementReference
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="className"></param>
        /// <param name="delay"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public ValueTask RemoveClassAsync(ElementReference elementReference, string className, int delay = 0, CancellationToken? cancellationToken = null)
            => _jsRuntime.InvokeVoidAsync("blazorStrap.RemoveClass", CancellationToken.None, elementReference, className, delay);

        // Not right in js yet
        public ValueTask RemoveEventAsync(string id, string name, EventTypes type, CancellationToken? cancellationToken = null)
            => _jsRuntime.InvokeVoidAsync("blazorStrap.RemoveEvent", CancellationToken.None, id, name, type.NameToLower());
        
        public ValueTask RemoveDocumentEventAsync(string id, string name, EventTypes type, CancellationToken? cancellationToken = null)
            => _jsRuntime.InvokeVoidAsync("blazorStrap.RemoveDocumentEvent", CancellationToken.None, id, name, type.NameToLower());
        
        /// <summary>
        /// Removes a Popover 
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        public ValueTask RemovePopoverAsync(ElementReference elementReference, string id, CancellationToken? cancellationToken = null)
            => _jsRuntime.InvokeVoidAsync("blazorStrap.RemovePopover", CancellationToken.None, elementReference, id);
        
        /// <summary>
        /// Sets a body style
        /// </summary>
        /// <param name="style"></param>
        /// <param name="value"></param>
        /// <param name="cancellationToken"></param>
        public ValueTask SetBodyStyleAsync(string style, string value, CancellationToken? cancellationToken = null)
            => _jsRuntime.InvokeVoidAsync("blazorStrap.SetBodyStyle", CancellationToken.None, style,value);
        
        /// <summary>
        /// Sets set css link for the theme switcher
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="version"></param>
        public ValueTask<bool> SetBootstrapCssAsync(string? theme, string version)
            => _jsRuntime.InvokeAsync<bool>("blazorStrap.SetBootstrapCss", theme, version);
        
        [Obsolete]
        public ValueTask<bool> SetBootstrapCss(string? theme, string version)
        {
            return _jsRuntime.InvokeAsync<bool>("blazorStrap.setBootstrapCss", theme, version);
        }
        
        /// <summary>
        /// Sets a style for the ElementReference
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="style"></param>
        /// <param name="value"></param>
        /// <param name="delay"></param>
        /// <param name="cancellationToken"></param>
        public ValueTask SetStyleAsync(ElementReference elementReference, string style, string value, int delay, CancellationToken? cancellationToken = null)
            => _jsRuntime.InvokeVoidAsync("blazorStrap.SetStyle", CancellationToken.None, elementReference, style,value, delay);
        
        /// <summary>
        /// Adds the toast timer to ElementReference
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="time"></param>
        /// <param name="timeRemaining"></param>
        /// <param name="rendered"></param>
        /// <param name="cancellationToken"></param>
        public ValueTask ToastTimerAsync(ElementReference elementReference, int time, int timeRemaining, bool rendered, CancellationToken? cancellationToken = null) 
            => _jsRuntime.InvokeVoidAsync("blazorStrap.ToastTimer", cancellationToken ?? CancellationToken.None, elementReference, time, timeRemaining, rendered);
        
        /// <summary>
        /// Returns true if no transition starts after given delay
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="delay"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>bool</returns>
        public ValueTask<bool> TransitionDidNotStartAsync(ElementReference elementReference, int delay = 200, CancellationToken? cancellationToken = null)
            => _jsRuntime.InvokeAsync<bool>("blazorStrap.SetStyle", CancellationToken.None, elementReference, delay);
        
        /// <summary>
        /// Calls Update on the popover
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="cancellationToken"></param>
        public ValueTask UpdatePopoverAsync(ElementReference elementReference, CancellationToken? cancellationToken = null)
            => _jsRuntime.InvokeVoidAsync("blazorStrap.UpdatePopover", CancellationToken.None, elementReference);
        
        /// <summary>
        /// Updates the placement of the popovers arrows
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="placement"></param>
        /// <param name="tooltip"></param>
        /// <param name="cancellationToken"></param>
        public ValueTask UpdatePopoverArrowAsync(ElementReference elementReference, Placement placement, bool tooltip, CancellationToken? cancellationToken = null)
            => _jsRuntime.InvokeVoidAsync("blazorStrap.UpdatePopoverArrow", CancellationToken.None, elementReference, placement.Name().ToDashSeperated(), tooltip);
        
        
        
        

        
        // Sync Methods

        /// <summary>
        /// Returns a string array of all child data-blazorstrap ids
        /// </summary>
        /// <param name="elementReference"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void GetChildrenIds(ElementReference elementReference)
        {
            if (_jSInProcessRuntime == null) throw InProcessError();
            _jSInProcessRuntime.Invoke<string[]>("blazorStrap.GetChildrenIds", elementReference);
        }
        
        /// <summary>
        /// Adds the toast timer to ElementReference
        /// </summary>
        /// <param name="elementReference"></param>
        /// <param name="time"></param>
        /// <param name="timeRemaining"></param>
        /// <param name="rendered"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void ToastTimer(ElementReference elementReference, int time, int timeRemaining, bool rendered)
        {
            if (_jSInProcessRuntime == null) throw InProcessError();
            _jSInProcessRuntime.InvokeVoid("blazorStrap.ToastTimer", elementReference, time, timeRemaining, rendered);
        }


      
        
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                }
                _disposedValue = true;
            }
        }
        private InvalidOperationException InProcessError() 
            => new InvalidOperationException("Javascript in process interop not available");
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}