using System.ComponentModel;

namespace BlazorStrap
{
    public enum DropdownDirection
    {
        Down,
        [Description("dropup")]
        Up,
        [Description("dropright")]
        Right,
        [Description("dropleft")]
        Left
    }
}
