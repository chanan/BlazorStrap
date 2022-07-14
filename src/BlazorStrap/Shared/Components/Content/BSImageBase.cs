using BlazorStrap.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Content
{
    public abstract class BSImageBase : BlazorStrapBase
    {
        /// <summary>
        /// Image alignment
        /// </summary>
        [Parameter] public Align Align { get; set; }

        /// <summary>
        /// Adds the <c>img-fluid</c> class.
        /// </summary>
        [Parameter] public bool IsFluid { get; set; }

        /// <summary>
        /// Adds the <c>rounded</c> class.
        /// </summary>
        [Parameter] public bool IsRounded { get; set; }

        /// <summary>
        /// Adds the <c>img-thumbnail</c> class.
        /// </summary>
        [Parameter] public bool IsThumbnail { get; set; }

        /// <summary>
        /// Outputs SVG placeholder.
        /// </summary>
        [Parameter] public bool IsPlaceholder { get; set; }

        /// <summary>
        /// Image source.
        /// </summary>
        [Parameter] public string? Source { get; set; }

        [CascadingParameter] public IBSFigure? Figure { get; set; }
        private int PlaceHolderHeight { get; set; } = 100;
        private int PlaceHolderWidth { get; set; } = 100;
        private string PlaceHolderText { get; set; } = "";

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }

        protected override void OnParametersSet()
        {
            if (Source == null) return;
            if (!IsPlaceholder && !Source.ToLower().Contains("x")) return;
            var data = Source.Split("x");
            PlaceHolderHeight = Convert.ToInt32(data[1]);
            PlaceHolderWidth = Convert.ToInt32(data[0]);
            PlaceHolderText = $"{data[0]}x{data[1]}";
            if (data.Length > 2)
                PlaceHolderText = data[2];
        }
    }
}
