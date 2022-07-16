using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSProgressBarBase : BlazorStrapBase, IDisposable
    {
        /// <summary>
        /// Sets the progress bar color.
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        /// <summary>
        /// Sets whether or not the progress bar stripes are animated.
        /// Only has effect when <see cref="IsStriped"/> is true.
        /// </summary>
        [Parameter] public bool IsAnimated { get; set; }

        /// <summary>
        /// Adds the progress-bar-striped to the progress bar to give stripes.
        /// </summary>
        [Parameter] public bool IsStriped { get; set; }

        /// <summary>
        /// Sets minimum value of progress bar. Defaults to 0.
        /// </summary>
        [Parameter] public double Min { get; set; } = 0;

        /// <summary>
        /// Sets maximum value of progress bar. Defaults to 100.
        /// </summary>
        [Parameter] public double Max { get; set; } = 100;

        /// <summary>
        /// Sets value of progress bar. Amount progress bar filled will be 
        /// proporational to <see cref="Min"/> and <see cref="Max"/>
        /// </summary>
        [Parameter]
        public double Value
        {
            get => _value;
            set => _value = value;
        }

        [CascadingParameter] public BSProgressBase? Parent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0032:Use auto property", Justification = "<Pending>")]
        private double _value;
        protected string? Width { get; set; } = null;

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
        protected string? Style { get; set; }

        protected override void OnInitialized()
        {
            if (Parent != null)
            {
                Parent.NotifyChildren += NotifyChildren;
                Parent.AddChild(this);
            }
        }
        protected override void OnParametersSet()
        {
            NotifyChildren();
            base.OnParametersSet();
        }
        private void NotifyChildren()
        {
            var percent = (_value - Min) / (Max - Min) * 100;

            Width = $"width:{percent.ToString(CultureInfo.InvariantCulture)}%;";
        }
        protected virtual void Dispose(bool disposing) { }
        public void Dispose()
        {
            if (Parent != null)
            {
                Parent.NotifyChildren -= NotifyChildren;
                Parent.RemoveChild(this);
                Parent.Refresh();

            }
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
