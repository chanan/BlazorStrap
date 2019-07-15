using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;

namespace BlazorStrap
{
    public class CodeBSTabList : BootstrapComponentBase
    {

        protected string classname =>
            new CssBuilder("nav")
                .AddClass("nav-tabs", !IsPills)
                .AddClass("nav-pills", IsPills)
                .AddClass("flex-column", IsVertical)
                .AddClass(GetAlignment())
                .AddClass(Class)
            .Build();

        protected string Tag => IsList ? "ul" : "nav";

        private bool _isList;
        [Parameter]
        internal bool IsList
        {
            get
            {
                return _isList;
            }
            set
            {
                _isList = value;
            }
        }
        [Parameter] private Alignment Alignment { get; set; } = Alignment.None;
        [Parameter] protected bool IsVertical { get; set; }
        [Parameter] protected bool IsPills { get; set; }
        [Parameter] protected bool IsFill { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }

        private string GetAlignment()
        {
            if (Alignment == Alignment.Center) { return "justify-content-center"; }
            if (Alignment == Alignment.Right) { return "justify-content-end"; }
            return null;
        }
    }
}
