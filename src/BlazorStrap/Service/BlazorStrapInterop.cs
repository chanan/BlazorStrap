using Microsoft.JSInterop;

namespace BlazorStrap.Service
{
    // TODO: Move all javascript calls here.
    public class BlazorStrapInterop : IDisposable
    {
        private IJSRuntime JsRuntime { get; }
        private bool _disposedValue;
        public BlazorStrapInterop(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }
        public ValueTask<bool> SetBootstrapCss(string? theme, string version)
        {
            return JsRuntime.InvokeAsync<bool>("blazorStrap.setBootstrapCss", theme, version);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}