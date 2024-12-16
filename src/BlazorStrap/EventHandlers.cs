using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    [EventHandler("ontransitionend", typeof(EventArgs))]
    [EventHandler("ontransitionrun", typeof(EventArgs))]
    [EventHandler("onanimationend", typeof(EventArgs))]
    public static class EventHandlers
    {
    }
}
