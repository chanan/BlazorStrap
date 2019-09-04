using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Timers;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class BSCarouselBase  : ComponentBase , IDisposable
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder("carousel slide")
        .AddClass(Class)
        .Build();

        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public int Interval { get; set; } = 5000;
        [Parameter] public bool PauseOnHover { get; set; }

        [Parameter] public int NumberOfItems { get; set; }
        [Parameter] public int ActiveIndex { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChangedEvent { get; set; }

        private Timer _timer;

        protected override void OnInitialized()
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
