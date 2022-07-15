using System.ComponentModel;

namespace BlazorStrap.V5_1
{
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
}
