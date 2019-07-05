using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap.util
{
    public class BlazorStrapInterop
    {
        private IJSRuntime JSRuntime { get; set; }

        public BlazorStrapInterop(IJSRuntime jsRuntime)
        {
            JSRuntime = jsRuntime;
        }

        public Task<bool> ChangeBody(string classname)
            => JSRuntime.InvokeAsync<bool>("blazorStrap.changeBody", classname);

        public Task<bool> Log(string message)
            => JSRuntime.InvokeAsync<bool>("blazorStrap.log", message);

        public Task<bool> Popper(string target, string popper, ElementRef arrow, string placement)
            => JSRuntime.InvokeAsync<bool>("blazorStrap.popper", target, popper, arrow, placement);

        public Task<bool> Tooltip(string target, ElementRef tooltip, ElementRef arrow, string placement)
            => JSRuntime.InvokeAsync<bool>("blazorStrap.tooltip", target, tooltip, arrow, placement);

        public Task FocusElement(ElementRef el) 
            => JSRuntime.InvokeAsync<object>("blazorStrap.focusElement", el);
    }
}