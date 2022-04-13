using System.Globalization;
using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSProgressBar : BlazorStrapBase, IDisposable
    {
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;
        [Parameter] public bool IsAnimated { get; set; }
        [Parameter] public bool IsStriped { get; set; }
        [Parameter] public double Min { get; set; } = 0;
        [Parameter] public double Max { get; set; } = 100;
        
        [Parameter] public double Value
        {
            get => _value;
            set => _value = value;
        }

        [CascadingParameter] public BSProgress? Parent { get;set;}

        private double _value;
        private string? Width { get; set; } = null;
        
        private string? ClassBuilder => new CssBuilder("progress-bar")
             .AddClass($"bg-{Color.NameToLower()}", Color != BSColor.Default)
             .AddClass("progress-bar-striped", IsStriped)
             .AddClass("progress-bar-animated", IsAnimated)
             .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
             .AddClass(Class, !string.IsNullOrEmpty(Class))
             .Build().ToNullString();
        private string? Style { get; set; }

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

            Width = $"width:{percent.ToString(CultureInfo.InvariantCulture)}%;" ;
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