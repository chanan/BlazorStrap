using System.ComponentModel;

namespace BlazorStrap
{
    public enum Color
    {
        None,
        [Description("active")]
        Active,
        [Description("primary")]
        Primary,
        [Description("secondary")]
        Secondary,
        [Description("success")]
        Success,
        [Description("danger")]
        Danger,
        [Description("warning")]
        Warning,
        [Description("info")]
        Info,
        [Description("light")]
        Light,
        [Description("dark")]
        Dark,
        [Description("link")]
        Link
    }
}