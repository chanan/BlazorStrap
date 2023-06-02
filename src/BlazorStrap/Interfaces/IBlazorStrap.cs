using BlazorStrap.Service;

namespace BlazorStrap
{
    public interface IBlazorStrap
    {

        BlazorStrapInterop JavaScriptInterop { get; }
        bool ShowDebugMessages { get; }
        Toaster Toaster { get; }
        public T CurrentTheme<T>() where T : Enum;
        Task SetBootstrapCss();
        Task SetBootstrapCss(string themeUrl);
        Task SetBootstrapCss<T>(T theme) where T : Enum;
        void ForwardClick(string id);
        Task InvokeEvent(string sender, string target, EventType type, object data);
    }
}