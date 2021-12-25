using Microsoft.JSInterop;

namespace BlazorStrap
{
    public class BlazorStrapService : IBlazorStrapService
    {
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
