using System.Threading.Tasks;

namespace BlazorStrap.Util
{
    class BootstrapCSS : IBootstrapCSS
    {
        private BlazorStrapInterop _blazorStrapInterop;

        public BootstrapCSS(BlazorStrapInterop blazorStrapInterop)
        {
            _blazorStrapInterop = blazorStrapInterop;
        }
        public Task SetBootstrapCSS()
        {
            return SetBootstrapCSS("bootstrap", "latest");
        }

        public Task SetBootstrapCSS(string version)
        {
            return SetBootstrapCSS("bootstrap", version);
        }

        public async Task SetBootstrapCSS(string theme, string version)
        {
            await _blazorStrapInterop.SetBootstrapCSS(theme.ToLower(), version);
        }

        public Task SetBootstrapCSS(Theme theme, string version)
        {
            return SetBootstrapCSS(theme.ToString().ToLower(), version);
        }
    }
}
