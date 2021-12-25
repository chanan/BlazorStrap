using System.ComponentModel;

namespace BlazorStrap
{
    public enum Gutters
    {
        [Description("Bootstrap Default")]
        Default,
        [Description("0")]
        None,
        [Description("1")]
        ExtraSmall,
        [Description("2")]
        Small,
        [Description("3")]
        Medium,
        [Description("4")]
        Large,
        [Description("5")]
        ExtraLarge
    }
    public enum Justify
    {
        [Description("Bootstrap Default")]
        Default,
        [Description("Justify Start")]
        Start,
        [Description("Justify Center")]
        Center,
        [Description("Justify End")]
        End,
        [Description("Justify Around")]
        Around,
        [Description("Justify Between")]
        Between,
        [Description("Justify Evenly")]
        Evenly
    }
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
}
