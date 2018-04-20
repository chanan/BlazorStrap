using System;
using Microsoft.AspNetCore.Blazor.Browser.Interop;

namespace BlazorStrap
{
    public class ExampleJsInterop
    {
        public static string Prompt(string message)
        {
            return RegisteredFunction.Invoke<string>(
                "BlazorStrap.ExampleJsInterop.Prompt",
                message);
        }
    }
}
