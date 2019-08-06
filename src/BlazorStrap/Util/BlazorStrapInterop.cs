using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorStrap.Util
{
    public class BlazorStrapInterop
    {
        public static EventHandler OnEscapeEvent { get; set; }
        private IJSRuntime JSRuntime { get; set; }

        public BlazorStrapInterop(IJSRuntime jsRuntime)
        {
            JSRuntime = jsRuntime;
        }

        [JSInvokable]
        public static Task OnEscape()
        {
            OnEscapeEvent?.Invoke(null, new EventArgs());
            return default;
        }
        public Task<bool> ChangeBody(string classname)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.changeBody", classname);
        }
        public Task<bool> ChangeBodyModal(string padding)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.changeBodyModal", padding);
        }

        public Task ModalEscapeKey()
        {
            return JSRuntime.InvokeAsync<string>("blazorStrap.modelEscape");
        }
        public Task<bool> Log(string message)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.log", message);
        }

        public Task<bool> Popper(string target, string popper, ElementRef arrow, string placement)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.popper", target, popper, arrow, placement);
        }

        public Task<bool> Tooltip(string target, ElementRef tooltip, ElementRef arrow, string placement)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.tooltip", target, tooltip, arrow, placement);
        }

        public Task FocusElement(ElementRef el)
        {
            return JSRuntime.InvokeAsync<object>("blazorStrap.focusElement", el);
        }

        public Task SetBootstrapCSS(string theme, string version)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.setBootstrapCSS", theme, version);
        }
    }
}