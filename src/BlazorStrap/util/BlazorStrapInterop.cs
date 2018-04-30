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
    }
}
