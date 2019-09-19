using BlazorComponentUtilities;
using BlazorStrap.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace BlazorStrap
{
    public abstract class BSCarouselBase : ComponentBase, IDisposable
    {
        
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        public List<BSCarouselItemBase> CarouselItems { get; set; } = new List<BSCarouselItemBase>();
        public List<BSCarouselIndicatorItemBase> CarouselIndicatorItems { get; set; } = new List<BSCarouselIndicatorItemBase>();
        private int LastNumberItems { get; set; } = 0;

        protected string classname =>
        new CssBuilder("carousel slide")
        .AddClass("carousel-fade", Fade)
        .AddClass(Class)
        .Build();
        [Inject] Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public int Interval { get; set; } = 5000;
        private string _pause { get; set; } = "hover";

        [Parameter]
        public string Pause
        {
            get
            {
                return _pause;
            }
            set
            {
                _pause = value;
                if (_pause != "hover")
                {
                    if (_pause == "true")
                        Timer?.Start();
                    else if (_pause == "false")
                        Timer?.Stop();
                }
            }
        }

        [Parameter] public bool Keyboard { get; set; } = true;
        [Parameter] public bool Wrap { get; set; } = true;
        [Parameter] public bool Fade { get; set; } = false;
        [Parameter] public bool Ride { get; set; } = false;
        [Parameter] public bool Touch { get; set; } = true;
        public bool AnimationRunning { get; set; } = false;
        private int _numberOfItems { get; set; }

        [Parameter]
        public int NumberOfItems
        {
            get { return _numberOfItems; }
            set
            {
                if (LastNumberItems != _numberOfItems)
                {
                    CarouselItems = new List<BSCarouselItemBase>();
                }
                _numberOfItems = value;
            }
        }

        [Parameter] public EventCallback<int> ActiveIndexChangedEvent { get; set; }
        internal Func<Task> ActiveIndexChanged { get; set; }
        internal int Direction { get; set; }
        [Parameter] public EventCallback OnSlide { get; set; }
        public int _activeIndex { get; set; }

        public int ActiveIndex
        {
            get { return _activeIndex; }
            set { _activeIndex = value;  ActiveIndexChanged.Invoke(); }
        }

        public Timer Timer { get; set; }

        protected override void OnInitialized()
        {
            if (Timer == null)
            {
                Timer = new Timer(Interval);
                Timer.Elapsed += OnTimerEvent;
                Timer.AutoReset = true;
                Timer.Start();
            }
        }

        protected override void OnParametersSet()
        {
            if (Ride && ActiveIndex == 0)
            {
                Timer.Start();
            }
        }

        private async void OnTimerEvent(object source, ElapsedEventArgs e)
        {
            if (AnimationRunning) return;
            if (ActiveIndex == NumberOfItems - 1 && (Ride || !Wrap))
            {
                Timer.Stop();
                return;
            }

            if (ActiveIndex == NumberOfItems - 1) { ActiveIndex = 0; }
            else { ActiveIndex = ActiveIndex + 1; }
            Direction = 0;
            await OnSlide.InvokeAsync(null);
            await ActiveIndexChangedEvent.InvokeAsync(ActiveIndex);
        }

        protected async Task OnKeyPress(KeyboardEventArgs e)
        {
            if (!Keyboard) return;
            if (e.Code == "37")
            {
                if (ActiveIndex - 1 < 0)
                    ActiveIndex = NumberOfItems - 1;
                else
                    ActiveIndex--;
            }
            else if (e.Code == "39")
            {
                if (ActiveIndex == NumberOfItems - 1)
                    ActiveIndex = 0;
                else
                    ActiveIndex++;
            }
            await ActiveIndexChangedEvent.InvokeAsync(ActiveIndex);
        }

        protected void OnMouseEnter()
        {
            if (_pause == "hover" && Timer != null) { Timer.Stop(); }
        }

        protected void OnMouseLeave()
        {
            if (_pause == "hover" && Timer != null) { ResetTimer(); }
        }

        public void ResetTimer()
        {
            Timer.Stop();
            Timer.Interval = CarouselItems[ActiveIndex].Interval;
            Timer.Start();
        }

        public async Task Refresh()
        {
            await InvokeAsync(StateHasChanged);
        }
        public void Dispose()
        {
            if (Timer != null)
            {
                Timer.Stop();
                Timer.Dispose();
                Timer = null;
            }
        }
    }
}