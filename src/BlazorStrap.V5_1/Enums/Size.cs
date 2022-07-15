using System.ComponentModel;

namespace BlazorStrap.V5_1
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
        ExtraLarge,
        [Description("xxl")]
        ExtraExtraLarge
    }
}
