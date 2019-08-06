using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Timers;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSCarousel : ComponentBase , IDisposable
    {
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder("carousel slide")
        .AddClass(Class)
        .Build();

        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [Parameter] protected int Interval { get; set; } = 5000;
        [Parameter] protected bool PauseOnHover { get; set; }

        [Parameter] protected int NumberOfItems { get; set; }
        [Parameter] protected int ActiveIndex { get; set; }
        [Parameter] protected EventCallback<int> ActiveIndexChangedEvent { get; set; }

        private Timer _timer;

        protected override void OnInit()
        {
            if (_timer == null)
            {
                _timer = new Timer(Interval);
                _timer.Elapsed += OnTimerEvent;
                _timer.AutoReset = true;
                _timer.Start();
            }
        }

        private async void OnTimerEvent(Object source, ElapsedEventArgs e)
        {
            if (ActiveIndex == NumberOfItems - 1) { ActiveIndex = 0; }
            else { ActiveIndex = ActiveIndex + 1; }

            await ActiveIndexChangedEvent.InvokeAsync(ActiveIndex);
        }

        protected void onmouseover()
        {
            if (PauseOnHover && _timer != null) { _timer.Stop(); }
        }

        protected void onmouseout()
        {
            if (PauseOnHover && _timer != null) { _timer.Start(); }
        }

        internal void FireEvent(int newIndex, int OldIndex)
        {

        }
        public void Dispose()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }
        }
    }
}
