using System.Threading.Tasks;

namespace BlazorStrap.Util
{
    public class Popper : IPopper
    {
        private readonly BlazorStrapInterop _blazorStrapInterop;

        public Popper(BlazorStrapInterop blazorStrapInterop)
        {
            _blazorStrapInterop = blazorStrapInterop;
        }

        public async Task SetPopper()
        {
            await _blazorStrapInterop.SetPopper();
        }
    }
}
