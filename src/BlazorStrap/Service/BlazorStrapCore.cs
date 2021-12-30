using Microsoft.JSInterop;

namespace BlazorStrap.Service
{
    public class BlazorStrapCore : IBlazorStrap
    {
        private readonly BlazorStrapInterop _blazorStrapInterop;
        //private readonly CurrentTheme _currentTheme;
        public Theme CurrentTheme { get; internal set; } = Theme.Bootstrap;

        public BlazorStrapCore(BlazorStrapInterop blazorStrapInterop)
        {
            _blazorStrapInterop = blazorStrapInterop;
        }
        public Task SetBootstrapCss()
        {
            CurrentTheme = Theme.Bootstrap;
            return SetBootstrapCss("bootstrap", "latest");
        }

        public Task SetBootstrapCss(string version)
        {
            CurrentTheme = Theme.Bootstrap;
            return SetBootstrapCss("bootstrap", version);
        }

        public async Task SetBootstrapCss(string? theme, string version)
        {
            theme = theme.FirstCharToUpper();
            if (theme == null) return;
            var enumTheme = (Theme)Enum.Parse(typeof(Theme), theme);
            CurrentTheme = enumTheme;
            await _blazorStrapInterop.SetBootstrapCss(theme.ToLowerInvariant(), version);
        }

        public Task SetBootstrapCss(Theme theme, string version)
        {
            CurrentTheme = theme;
            return SetBootstrapCss(theme.ToString().ToLowerInvariant(), version);
        }
        
        // All This needs cleaned up
        
        [JSInvokable("ModalBackdropClick")]
        public static void ModalBackdropClick()
        {
            ModalChange?.Invoke(null, true);
        }

        internal static event Action<BSModal?, bool>? ModalChange;

        public static void ModalChanged(BSModal obj)
        {
            ModalChange?.Invoke(obj, false);
        }

        internal void OnForwardClick(string id)
        {
            JSCallback.EventCallback(id, "clickforward", "click");
        }
    }
}
