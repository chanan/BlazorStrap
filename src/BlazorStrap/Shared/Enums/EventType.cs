using System.Text.Json.Serialization;
using BlazorStrap.JsonConverters;

namespace BlazorStrap
{
    [JsonConverter(typeof(EventTypeJsonConverter))]
    public enum EventType
    {
        Default,
        Focusout,
        Toggle,
        Mouseover,
        Mouseout,
        Mouseenter,
        Mouseleave,
        TransitionEnd, 
        Keyup,
        Resize,
        Click,
        Hide,
        Show,
        Sync

    }
}