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
        public ValueTask<bool> ChangeBody(string classname)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.changeBody", classname);
        }
        public ValueTask<bool> ChangeBodyModal(string padding)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.changeBodyModal", padding);
        }

        public ValueTask<string> ModalEscapeKey()
        {
            return JSRuntime.InvokeAsync<string>("blazorStrap.modelEscape");
        }
        public ValueTask<bool> Log(string message)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.log", message);
        }

        public ValueTask<bool> Popper(string target, string popper, ElementReference arrow, string placement)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.popper", target, popper, arrow, placement);
        }

        public ValueTask<bool> Tooltip(string target, ElementReference tooltip, ElementReference arrow, string placement)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.tooltip", target, tooltip, arrow, placement);
        }

        public ValueTask<object> FocusElement(ElementReference el)
        {
            return JSRuntime.InvokeAsync<object>("blazorStrap.focusElement", el);
        }

        public ValueTask<bool> SetBootstrapCSS(string theme, string version)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.setBootstrapCSS", theme, version);
        }
    }
}