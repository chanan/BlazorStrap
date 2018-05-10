using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Browser.Interop;

namespace BlazorStrap.util
{
    public class BlazorStrapInterop
    {
        public static bool ChangeBody(string classname)
        {
            return RegisteredFunction.Invoke<bool>("BlazorStrap.BlazorStrapInterop.ChangeBody", classname);
        }
        public static bool Log(string message)
        {
            return RegisteredFunction.Invoke<bool>("BlazorStrap.BlazorStrapInterop.Log", message);
        }
        public static bool Popper(string target, string popper, ElementRef arrow, string placement)
        {
            return RegisteredFunction.Invoke<bool>("BlazorStrap.BlazorStrapInterop.Popper", target, popper, arrow, placement);
        }
        public static bool Tooltip(string target, ElementRef tooltip, ElementRef arrow, string placement)
        {
            return RegisteredFunction.Invoke<bool>("BlazorStrap.BlazorStrapInterop.Tooltip", target, tooltip, arrow, placement);
        }
    }
}
