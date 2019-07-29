using System;
using System.Threading.Tasks;

namespace BlazorStrap.Util
{
    class BootstrapCSS : IBootstrapCSS
    {
        private BlazorStrapInterop _blazorStrapInterop;
        private CurrentTheme _currentTheme;

        public BootstrapCSS(BlazorStrapInterop blazorStrapInterop, CurrentTheme currentTheme)
        {
            _blazorStrapInterop = blazorStrapInterop;
            _currentTheme = currentTheme;
        }
        public Task SetBootstrapCSS()
        {
            _currentTheme.Theme = Theme.Bootstrap;
            return SetBootstrapCSS("bootstrap", "latest");
        }

        public Task SetBootstrapCSS(string version)
        {
            _currentTheme.Theme = Theme.Bootstrap;
            return SetBootstrapCSS("bootstrap", version);
        }

        public async Task SetBootstrapCSS(string theme, string version)
        {
            Theme enumTheme = (Theme)Enum.Parse(typeof(Theme), theme.FirstCharToUpper());
            _currentTheme.Theme = enumTheme;
            await _blazorStrapInterop.SetBootstrapCSS(theme.ToLower(), version);
        }

        public Task SetBootstrapCSS(Theme theme, string version)
        {
            _currentTheme.Theme = theme;
            return SetBootstrapCSS(theme.ToString().ToLower(), version);
        }

        public Theme CurrentTheme
        {
            get
            {
                return _currentTheme.Theme;
            }
        }
    }
}
