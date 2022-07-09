namespace BlazorStrap.Service
{
    public class BlazorStrapCore : IBlazorStrap
    {
        internal readonly BlazorStrapInterop Interop;
        internal BootstrapVersion BootstrapVersion;
        public bool ShowDebugMessages { get; private set; }
        internal Func<string, CallerName, EventType, Task>? OnEventForward;
        
        //private readonly CurrentTheme _currentTheme;
        public Toaster Toaster { get;} = new Toaster();
        public Theme CurrentTheme { get; internal set; } = Theme.Bootstrap;

        public BlazorStrapCore(BlazorStrapInterop? interop, string basepath)
        {
            if(interop == null)
                throw new ArgumentNullException(nameof(interop));
            Interop = interop;
        }
        public BlazorStrapCore(BlazorStrapInterop interop, Action<BlazorStrapOptions>? buildOptions)
        {
            var options = new BlazorStrapOptions();
            if (buildOptions != null)
            {
                buildOptions.Invoke(options);
            }
            Interop = interop;
            BootstrapVersion = options.BootstrapVersion;
            ShowDebugMessages = options.ShowDebugMessages;
        }
        public Task SetBootstrapCss()
        {
            CurrentTheme = Theme.Bootstrap;
            return SetBootstrapCss("bootstrap", "latest");
        }

        public Task SetBootstrapCss(string version)
        {
            CurrentTheme = Theme.Bootstrap;
            BootstrapVersion = GetBootstrapVersion(version);
            return SetBootstrapCss("bootstrap", version);
        }

        public async Task SetBootstrapCss(string? theme, string version)
        {
            BootstrapVersion = GetBootstrapVersion(version);
            theme = theme.FirstCharToUpper();
            if (theme == null) return;
            var enumTheme = (Theme)Enum.Parse(typeof(Theme), theme);
            CurrentTheme = enumTheme;
            await Interop.SetBootstrapCssAsync(theme.ToLowerInvariant(), version);
        }

        public Task SetBootstrapCss(Theme theme, string version)
        {
            BootstrapVersion = GetBootstrapVersion(version);
            CurrentTheme = theme;
            return SetBootstrapCss(theme.ToString().ToLowerInvariant(), version);
        }
        
        internal static event Action<BSModal?, bool>? ModalChange;

        public static void ModalChanged(BSModal obj)
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
        private BootstrapVersion GetBootstrapVersion(string version)
        {
            if(version.Substring(0,1) == "4")
            {
                return BootstrapVersion.Bootstrap4;
            }
            return BootstrapVersion.Bootstrap5;
        }
    }
}
