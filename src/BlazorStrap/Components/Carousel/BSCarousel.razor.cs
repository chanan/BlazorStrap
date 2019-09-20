using BlazorComponentUtilities;
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
        private const string _carouselFade = "carousel-fade";

        private int _activeIndex { get; set; }

        public int ActiveIndex
        {
            get { return _activeIndex; }
            set { _activeIndex = value; ActiveIndexChanged.Invoke(); }
        }

        [Parameter] public EventCallback<int> ActiveIndexChangedEvent { get; set; }
        public bool AnimationRunning { get; set; } = false;
        public List<BSCarouselIndicatorItemBase> CarouselIndicatorItems { get; } = new List<BSCarouselIndicatorItemBase>();
        public List<BSCarouselItemBase> CarouselItems { get; } = new List<BSCarouselItemBase>();
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public bool Fade { get; set; } = false;
        [Parameter] public int Interval { get; set; } = 5000;
        [Parameter] public bool Keyboard { get; set; } = true;

        [Parameter]
        public int NumberOfItems
        {
            get { return _numberOfItems; }
            set
            {
                if (_lastNumberItems != _numberOfItems)
                {
                    CarouselItems.Clear();
                }
                _numberOfItems = value;
            }
        }

        [Parameter] public EventCallback OnSlide { get; set; }

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

        [Parameter] public bool Ride { get; set; } = false;
        public Timer Timer { get; set; }
        [Parameter] public bool Touch { get; set; } = true;
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Parameter] public bool Wrap { get; set; } = true;
        internal Func<Task> ActiveIndexChanged { get; set; }
        internal int Direction { get; set; }

        protected string Classname => new CssBuilder("carousel slide")
        .AddClass(_carouselFade, Fade)
        .AddClass(Class)
        .Build();

        private int _numberOfItems { get; set; }
        private string _pause { get; set; } = "hover";
        [Inject] protected Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }
        private int _lastNumberItems { get; set; } = 0;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Timer != null)
                {
                    Timer.Stop();
                    Timer.Dispose();
                }
            }
        }

        public async Task Refresh()
        {
            await InvokeAsync(StateHasChanged).ConfigureAwait(false);
        }

        public void ResetTimer()
        {
            Timer.Stop();
            Timer.Interval = CarouselItems[ActiveIndex].Interval;
            Timer.Start();
        }

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

        protected async Task OnKeyPress(KeyboardEventArgs e)
        {
            if (!Keyboard) return;
            if (e?.Code == "37")
            {
                if (ActiveIndex - 1 < 0)
                    ActiveIndex = NumberOfItems - 1;
                else
                    ActiveIndex--;
            }
            else if (e?.Code == "39")
            {
                if (ActiveIndex == NumberOfItems - 1)
                    ActiveIndex = 0;
                else
                    ActiveIndex++;
            }
            await ActiveIndexChangedEvent.InvokeAsync(ActiveIndex).ConfigureAwait(false);
        }

        protected void OnMouseEnter()
        {
            if (_pause == "hover" && Timer != null) { Timer.Stop(); }
        }

        protected void OnMouseLeave()
        {
            if (_pause == "hover" && Timer != null) { ResetTimer(); }
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

            ActiveIndex = ActiveIndex == NumberOfItems - 1 ? 0 : ActiveIndex + 1;
            Direction = 0;
            await OnSlide.InvokeAsync(null).ConfigureAwait(false);
            await ActiveIndexChangedEvent.InvokeAsync(ActiveIndex).ConfigureAwait(false);
        }
    }
}
