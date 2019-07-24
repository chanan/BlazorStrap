using System.ComponentModel;

namespace BlazorStrap
{
    public enum Placement
    {
        [Description("auto")]
        Auto,
        [Description("auto-start")]
        AutoStart,
        [Description("auto-end")]
        AutoEnd,
        [Description("top")]
        Top,
        [Description("top-start")]
        TopStart,
        [Description("top-end")]
        TopEnd,
        [Description("right")]
        Right,
        [Description("right-start")]
        RightStart,
        [Description("right-end")]
        EightEnd,
        [Description("bottom")]
        Bottom,
        [Description("bottom-start")]
        BottomStart,
        [Description("bottom-end")]
        BottomEnd,
        [Description("left")]
        Left,
        [Description("left-start")]
        LeftStart,
        [Description("left-end")]
        LeftEnd
    }
}
