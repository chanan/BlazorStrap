using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorStrap.Extensions.BSDataTable
{
    public partial class BSDataCarousel<TItem> : DataComponentBase<TItem>
    {
        // Private Variables
        private int _numberOfItems => Items.Count;

        /// <summary>
        /// When true, this adds the Indicators to the Carousel.
        /// </summary>
        [Parameter] public bool HasIndicators { get; set; } = false;

        /// <summary>
        /// When true, this adds the previous and next controls to the Carousel.
        /// </summary>
        [Parameter] public bool HasControls { get; set; } = false;


        // Pass through properties for the BSCarousel
        [Parameter] public string Class { get; set; }
        [Parameter] public bool Fade { get; set; } = false;
        [Parameter] public int Interval { get; set; } = 5000;
        [Parameter] public bool Keyboard { get; set; } = true;
        [Parameter] public bool Ride { get; set; } = false;
        [Parameter] public bool Touch { get; set; } = true;
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Parameter] public bool Wrap { get; set; } = true;
    }
}
