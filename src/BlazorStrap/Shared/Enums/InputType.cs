using System.ComponentModel;

namespace BlazorStrap
{
    public enum InputType
    {
        [Description("color")]
        Color,
        [Description("date")]
        Date,
        [Description("datetime-local")]
        DateTimeLocal,
        [Description("email")]
        Email,
        [Description("month")]
        Month,
        [Description("number")]
        Number,
        [Description("password")]
        Password,
        [Description("range")]
        Range,
        [Description("search")]
        Search,
        [Description("select")]
        Select,
        [Description("tel")]
        Tel,
        [Description("text")]
        Text,
        [Description("textarea")]
        TextArea,
        [Description("time")]
        Time,
        [Description("url")]
        Url,
        [Description("week")]
        Week,
        [Description("datalist")]
        DataList
    }
}