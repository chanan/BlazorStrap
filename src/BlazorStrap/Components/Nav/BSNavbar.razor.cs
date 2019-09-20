using BlazorComponentUtilities;
using BlazorStrap.Util;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Timers;

namespace BlazorStrap
{
    public abstract class BSNavbarBase : ComponentBase, IDisposable
    {
        private Timer _timer = new Timer(250);
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public Color Color { get; set; } = Color.None;
        [Parameter] public bool IsDark { get; set; }
        [Parameter] public bool IsExpand { get; set; }
        [Parameter] public bool IsFixedBottom { get; set; }
        [Parameter] public bool IsFixedTop { get; set; }
        [Parameter] public bool IsStickyTop { get; set; }
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        internal bool HasCollapsed { get; set; }
        internal bool Visable
        {
            get { return _visable; }
            set
            {
                VisableChange.Invoke(this, value);
                _visable = value;
            }
        }

        internal EventHandler<bool> VisableChange { get; set; }
        protected string Classname =>
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

        private bool _visable { get; set; }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal void GotFocus()
        {
            _timer.Stop();
            _timer.Interval = 250;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timer.Dispose();
            }
        }

        protected void LostFocus()
        {
            _timer.Start();
        }

        protected override void OnInitialized()
        {
            _timer.Elapsed += OnTimedEvent;
        }
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (HasCollapsed)
            {
                Visable = false;
            }

            _timer.Stop();
            _timer.Interval = 250;
        }
    }
}
