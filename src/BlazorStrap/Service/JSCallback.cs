using BlazorStrap.Utilities;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public static class JSCallback
    {
        public static void CloseOtherDropdowns(BSDropdown sender)
        {
            DropdownDocumentClickEvent?.Invoke(sender);
        }

        [JSInvokable("DropdownDocumentClick")]
        public static void DropdownDocumentClick()
        {
            DropdownDocumentClickEvent?.Invoke(null);
        }

        [JSInvokable("EventCallback")]
        public static void EventCallback(string id, string name, string type, Dictionary<string, string>? classList = null, JavascriptEvent? e = null)
        {

            EventHandler?.Invoke(id, name, type, classList, e);
        }
        
        [JSInvokable("EventFallback")]
        public static void EventFallback(string id, string name, string type)
        {

            EventHandler?.Invoke(id, name, type, new Dictionary<string, string>(), new JavascriptEvent());
        }
        [JSInvokable("ResizeComplete")]
        public static void ResizeComplete(int width)
        {
            ResizeEvent?.Invoke(width);
        }

        internal static event Action<BSDropdown?>? DropdownDocumentClickEvent;
        internal static event Action<int>? ResizeEvent;
        internal static event Action<string, string, string, Dictionary<string, string>?, JavascriptEvent?>? EventHandler;
    }
}
