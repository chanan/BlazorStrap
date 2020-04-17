using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Extensions.DataComponents
{
    public partial class BSDataCarousel<TItem>
    {
        // Private Variables
        private bool _isLoading => Items == null;
        private int _numberOfItems => Items.Count;
        private bool _hasNoData => _numberOfItems == 0;

        /// <summary>
        /// Data model to pass into the component
        /// </summary>
        [Parameter] public List<TItem> Items { get; set; }

        /// <summary>
        /// This template loops through each row of your data and adds it to the Carousel
        /// </summary>
        [Parameter] public RenderFragment<TItem> ItemTemplate { get; set; }

        /// <summary>
        /// This template will be displayed if the list of your data is null, or not initialized.
        /// It will be displayed instead of the carousel.
        /// </summary>
        [Parameter] public RenderFragment LoadingTemplate { get; set; }

        /// <summary>
        /// This template will be displayed if the list of your data has no items.
        /// It will be displayed instead of the carousel.
        /// </summary>
        [Parameter] public RenderFragment NoDataTemplate { get; set; }

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
