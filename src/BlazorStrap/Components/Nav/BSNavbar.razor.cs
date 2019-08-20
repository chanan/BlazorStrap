using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Timers;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSNavbar : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        private System.Timers.Timer _timer = new System.Timers.Timer(250);
        
        private bool _visable { get; set; }
        
        protected private string classname =>
        new CssBuilder("navbar")
            .AddClass("fixed-top", IsFixedTop)
            .AddClass("fixed-bottom", IsFixedBottom)
            .AddClass("sticky-top", IsStickyTop)
            .AddClass("navbar-dark", IsDark)
            .AddClass("navbar-light", !IsDark)
            .AddClass($"bg-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass("navbar-expand-lg", IsExpand)
            .AddClass(Class)
        .Build();

        [Parameter] public Color Color { get; set; } = Color.None;
        [Parameter] public bool IsDark { get; set; }
        [Parameter] public bool IsExpand { get; set; }

        [Parameter] public bool IsFixedTop { get; set; }
        [Parameter] public bool IsFixedBottom { get; set; }
        [Parameter] public bool IsStickyTop { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        internal bool HasCollapsed { get; set; }
        internal EventHandler<bool> VisableChange { get; set; }
        internal bool Visable
        {
            get { return _visable; }
            set {
                VisableChange.Invoke(this, value );
                _visable = value;
            }
        }
        

        protected override void OnInitialized()
        {
            _timer.Elapsed += OnTimedEvent;
        }

        internal void GotFocus()
        {
            _timer.Stop();
            _timer.Interval = 250;
        }
        protected void LostFocus()
        {
            _timer.Start();
        }


        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if(HasCollapsed)
            {
                Visable = false;
            }
            
            _timer.Stop();
            _timer.Interval = 250;
        }
    }
}
