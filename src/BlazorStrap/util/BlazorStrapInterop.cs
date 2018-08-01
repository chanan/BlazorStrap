using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;
using Microsoft.JSInterop;

namespace BlazorStrap.util
{
    public class BlazorStrapInterop
    {
        public static Task<bool> ChangeBody(string classname)
        {
            return JSRuntime.Current.InvokeAsync<bool>("blazorStrap.changeBody", classname);
        }
        public static Task<bool> Log(string message)
        {
            return JSRuntime.Current.InvokeAsync<bool>("blazorStrap.log", message);
        }
        public static Task<bool> Popper(string target, string popper, ElementRef arrow, string placement)
        {
            return JSRuntime.Current.InvokeAsync<bool>("blazorStrap.popper", target, popper, arrow, placement);
        }
        public static Task<bool> Tooltip(string target, ElementRef tooltip, ElementRef arrow, string placement)
        {
            return JSRuntime.Current.InvokeAsync<bool>("blazorStrap.tooltip", target, tooltip, arrow, placement);
        }
    }
}
