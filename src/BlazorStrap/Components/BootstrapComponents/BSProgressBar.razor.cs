using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSProgressBar : BlazorStrapBase, IDisposable
    {
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;
        [Parameter] public bool IsAnimated { get; set; }
        [Parameter] public bool IsStriped { get; set; }
        [Parameter] public int Max { get; set; } = 100;

        [Parameter]
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                if(Parent != null && Parent?.Children > 1)
                {
                    if (value == 0) { Style = null; }
                    var percent = Math.Round((value / (double)Max * 100) - (Parent.Children / 100));
                    Style = $"width: {percent}%; {Style}".Trim();
                }
                else
                {
                    if (value == 0) { Style = null; }
                    var percent = Math.Round((value / (double)Max * 100));
                    Style = $"width: {percent}%; {Style}".Trim();
                }
                _value = value;
            }
        }

        [CascadingParameter] public BSProgress Parent { get;set;}

        internal int _value { get; set; }

        internal string? ClassBuilder => new CssBuilder("progress-bar")
             .AddClass($"bg-{BSColor.GetName<BSColor>(Color).ToLower()}", Color != BSColor.Default)
             .AddClass("progress-bar-striped", IsStriped)
             .AddClass("progress-bar-animated", IsAnimated)
             .AddClass(LayoutClass, !String.IsNullOrEmpty(LayoutClass))
             .AddClass(Class, !String.IsNullOrEmpty(Class))
             .Build().ToNullString();

        internal string? Style { get; set; }

        protected override void OnInitialized()
        {
            if(Parent == null)
            Parent.Children++;
        }

        public void Dispose()
        {
            if (Parent == null)
            {
                Parent.Children--;
                if (Parent.Children < 0)
                    Parent.Children = 0;
                Parent.Refresh();
            }
        }
    }
}