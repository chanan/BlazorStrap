using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Modal;

namespace BlazorStrap.Service
{
    public class BlazorStrapCore : IBlazorStrap
    {
        public readonly BlazorStrapInterop Interop;
        public readonly Interop NewInterop;
        public bool ShowDebugMessages { get; private set; }
        internal Func<string, CallerName, EventType, Task>? OnEventForward;
        
        //private readonly CurrentTheme _currentTheme;
        public Toaster Toaster { get;} = new Toaster();
        private string _currentTheme  = "bootstrap";
        public T CurrentTheme<T>() where T : Enum
        {
            return (T) Enum.Parse(typeof(T), _currentTheme, true);
        }

        public BlazorStrapCore(BlazorStrapInterop? interop,  string basepath, Interop newInterop)
        {
            if (interop == null)
                throw new ArgumentNullException(nameof(interop));
            Interop = interop;
            NewInterop = newInterop;
        }
        public BlazorStrapCore(BlazorStrapInterop interop, Action<BlazorStrapOptions>? buildOptions, Interop newInterop)
        {
            var options = new BlazorStrapOptions();
            if (buildOptions != null)
            {
                buildOptions.Invoke(options);
            }
            Interop = interop;
            NewInterop = newInterop;
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
