using System.ComponentModel;

namespace BlazorStrap
{
    public enum Align
    {
        [Description("Bootstrap Default")]
        Default,
        [Description("Align Start")]
        Start,
        [Description("Align Center")]
        Center,
        [Description("Align End")]
        End,
    }
    public enum AlignRow
    {
        [Description("Bootstrap Default")]
        Default,
        [Description("Align Top")]
        Top,
        [Description("Align Middle")]
        Middle,
        [Description("Align Bottom")]
        Bottom,
    }
    public enum VerticalAlignment
    {
        None,
        Bottom,
        Center,
        Top
    }
    public enum Alignment
    {
        None,
        Left,
        Center,
        Right
    }
}
