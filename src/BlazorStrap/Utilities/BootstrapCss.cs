using System;
using System.Threading.Tasks;

namespace BlazorStrap.Utilities
{
    /*
    class BootstrapCss : IBootstrapCss, IBootstrapCSS
    {
        private readonly BlazorStrapInterop _blazorStrapInterop;
        private readonly CurrentTheme _currentTheme;

        public BootstrapCss(BlazorStrapInterop blazorStrapInterop, CurrentTheme currentTheme)
        {
            _blazorStrapInterop = blazorStrapInterop;
            _currentTheme = currentTheme;
        }
        public Task SetBootstrapCss()
        {
            _currentTheme.Theme = Theme.Bootstrap;
            return SetBootstrapCss("bootstrap", "latest");
        }

        public Task SetBootstrapCss(string version)
        {
            _currentTheme.Theme = Theme.Bootstrap;
            return SetBootstrapCss("bootstrap", version);
        }

        public async Task SetBootstrapCss(string theme, string version)
        {
            var enumTheme = (Theme)Enum.Parse(typeof(Theme), theme.FirstCharToUpper());
            _currentTheme.Theme = enumTheme;
            await _blazorStrapInterop.SetBootstrapCss(theme.ToLowerInvariant(), version);
        }

        public Task SetBootstrapCss(Theme theme, string version)
        {
            _currentTheme.Theme = theme;
            return SetBootstrapCss(theme.ToString().ToLowerInvariant(), version);
        }

        public Theme CurrentTheme => _currentTheme.Theme;

        #region "Legacy / Obsolete Implementations from IBootstrapCSS"

        public Task SetBootstrapCSS() => SetBootstrapCss();
        public Task SetBootstrapCSS(string version) => SetBootstrapCss(version);
        public Task SetBootstrapCSS(string theme, string version) => SetBootstrapCss(theme, version);
        public Task SetBootstrapCSS(Theme theme, string version) => SetBootstrapCss(theme, version);

        #endregion
    
    }
    */
}
