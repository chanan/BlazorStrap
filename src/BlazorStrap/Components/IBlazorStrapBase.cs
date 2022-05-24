namespace BlazorStrap
{
    public interface IBlazorStrapBase
    {
        /// <summary>
        /// Css classes to pass to item.
        /// </summary>
        string Class { get; set; }

        /// <summary>
        /// data-blazorstrap value. Used to uniquely identify element on a page.
        /// </summary>
        string DataId { get; set; }

        /// <summary>
        /// Top, Bottom, Left, Right Margins
        /// </summary>
        Margins Margin { get; set; }

        /// <summary>
        /// Bottom Margin
        /// </summary>
        Margins MarginBottom { get; set; }

        /// <summary>
        /// End/Right Margin
        /// </summary>
        Margins MarginEnd { get; set; }

        /// <summary>
        /// Left and Right Margins
        /// </summary>
        Margins MarginLeftAndRight { get; set; }

        /// <summary>
        /// Start/Left Margin
        /// </summary>
        Margins MarginStart { get; set; }

        /// <summary>
        /// Top Margin
        /// </summary>
        Margins MarginTop { get; set; }

        /// <summary>
        /// Top and Bottom Margins
        /// </summary>
        Margins MarginTopAndBottom { get; set; }

        /// <summary>
        /// Top, Bottom, Left, Right Padding
        /// </summary>
        Padding Padding { get; set; }

        /// <summary>
        /// Bottom Padding
        /// </summary>
        Padding PaddingBottom { get; set; }

        /// <summary>
        /// End/Right Padding
        /// </summary>
        Padding PaddingEnd { get; set; }

        /// <summary>
        /// Left and Right Padding
        /// </summary>
        Padding PaddingLeftAndRight { get; set; }

        /// <summary>
        /// Start/Left Padding
        /// </summary>
        Padding PaddingStart { get; set; }

        /// <summary>
        /// Top Padding
        /// </summary>
        Padding PaddingTop { get; set; }

        /// <summary>
        /// Top and Bottom Padding
        /// </summary>
        Padding PaddingTopAndBottom { get; set; }


        /// <summary>
        /// Position Helper
        /// </summary>
        Position Position { get; set; }

        string? LayoutClass { get; }
    }
}