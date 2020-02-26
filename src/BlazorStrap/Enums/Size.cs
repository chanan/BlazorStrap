using System.ComponentModel;

namespace BlazorStrap
{
    public enum Size
    {
        None,
        [Description("xs")]
        ExtraSmall,
        [Description("sm")]
        Small,
        [Description("md")]
        Medium,
        [Description("lg")]
        Large,
        [Description("xl")]
        ExtraLarge
    }
}
