using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;

namespace BlazorStrap
{
    [EventHandler("onmouseleave", typeof(MouseEventArgs))]
    [EventHandler("onmouseenter", typeof(MouseEventArgs))]
    [EventHandler("ontransitionend", typeof(EventArgs))]
    [EventHandler("ontransitionrun", typeof(EventArgs))]
    [EventHandler("onanimationend", typeof(EventArgs))]
    public static class EventHandlers
    {
    }
}
