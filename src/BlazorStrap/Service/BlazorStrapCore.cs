namespace BlazorStrap.Service
{
    public class BlazorStrapCore : IBlazorStrap
    {
        internal readonly BlazorStrapInterop Interop;
        internal BootStrapVersion bootStrapVersion;
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
        public BlazorStrapCore(BlazorStrapInterop interop, BootStrapVersion version)
        {
            Interop = interop;
            bootStrapVersion = version;
        }
        public Task SetBootstrapCss()
        {
            CurrentTheme = Theme.Bootstrap;
            return SetBootstrapCss("bootstrap", "latest");
        }

        public Task SetBootstrapCss(string version)
        {
            CurrentTheme = Theme.Bootstrap;
            bootStrapVersion = GetBootstrapVersion(version);
            return SetBootstrapCss("bootstrap", version);
        }

        public async Task SetBootstrapCss(string? theme, string version)
        {
            bootStrapVersion = GetBootstrapVersion(version);
            theme = theme.FirstCharToUpper();
            if (theme == null) return;
            var enumTheme = (Theme)Enum.Parse(typeof(Theme), theme);
            CurrentTheme = enumTheme;
            await Interop.SetBootstrapCssAsync(theme.ToLowerInvariant(), version);
        }

        public Task SetBootstrapCss(Theme theme, string version)
        {
            bootStrapVersion = GetBootstrapVersion(version);
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
        private BootStrapVersion GetBootstrapVersion(string version)
        {
            Console.WriteLine(version.Substring(0,1));
            if(version.Substring(0,1) == "4")
            {
                return BootStrapVersion.Bootstrap4;
            }
            return BootStrapVersion.Bootstrap5;
        }
    }
}
