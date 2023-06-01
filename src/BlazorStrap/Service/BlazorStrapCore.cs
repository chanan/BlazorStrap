using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Modal;
using BlazorStrap.Shared.Components.OffCanvas;
using Microsoft.JSInterop;

namespace BlazorStrap.Service
{
    public class BlazorStrapCore : IBlazorStrap
    {
        public readonly BlazorStrapInterop Interop;
        public BSInterop JavaScriptInterop { get; }
        
        public bool ShowDebugMessages { get; private set; }
        public Func<string, string, EventType, object, Task>? OnEvent { get; set; }

        internal Func<string, CallerName, EventType, Task>? OnEventForward;
        
        //private readonly CurrentTheme _currentTheme;
        public Toaster Toaster { get;} = new Toaster();
        private string _currentTheme  = "bootstrap";
        public T CurrentTheme<T>() where T : Enum
        {
            return (T) Enum.Parse(typeof(T), _currentTheme, true);
        }

        public BlazorStrapCore(BlazorStrapInterop? interop,  string basepath, IJSRuntime jSRuntime)
        {
            if (interop == null)
                throw new ArgumentNullException(nameof(interop));
            Interop = interop;
            JavaScriptInterop = new BSInterop(jSRuntime, this);
        }
        public BlazorStrapCore(BlazorStrapInterop interop, Action<BlazorStrapOptions>? buildOptions, IJSRuntime jSRuntime)
        {
            var options = new BlazorStrapOptions();
            if (buildOptions != null)
            {
                buildOptions.Invoke(options);
            }
            Interop = interop;
            JavaScriptInterop = new BSInterop(jSRuntime, this);
            ShowDebugMessages = options.ShowDebugMessages;
        }
        public Task SetBootstrapCss()
        {
            _currentTheme = "bootstrap";
            return SetBootstrapCss("bootstrap", "latest");
        }

        public Task SetBootstrapCss(string version)
        {
            _currentTheme = "bootstrap";
            return SetBootstrapCss("bootstrap", version);
        }

        public async Task SetBootstrapCss(string? theme, string version)
        {
            theme = theme.FirstCharToUpper();
            if (theme == null) return;
            _currentTheme = theme.ToLowerInvariant();
            try
            {
                await Interop.SetBootstrapCssAsync(theme.ToLowerInvariant(), version);
            }
            catch { }
        }

        public Task SetBootstrapCss<T>(T theme, string version) where T : Enum
        {
            _currentTheme = theme.NameToLower();
            return SetBootstrapCss(theme.ToString().ToLowerInvariant(), version);
        }
        
        internal event Action<BSModalBase?, bool>? ModalChange;

        public void ModalChanged(BSModalBase obj)
        {
            ModalChange?.Invoke(obj, false);
        }

        public async Task InvokeEvent(string sender, string target, EventType type, object data)
        {
            if(OnEvent is not null)
                await OnEvent.Invoke(sender, target, type, data);
        }

        public void ForwardClick(string id)
        {
            OnEventForward?.Invoke(id, new CallerName(typeof(ClickForward).Name.ToLower()), EventType.Click );
        }
        internal void ForwardToggle<T>(string id, T name) where T: class
        {
            OnEventForward?.Invoke(id, new CallerName(typeof(T).Name.ToLower()), EventType.Toggle );
        }
        
    }
}
