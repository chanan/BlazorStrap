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
            get => _activeIndex;
            set { _activeIndex = value; ActiveIndexChanged?.Invoke(); DoAnimations(); }
        }

        private bool _timerEnabled { get; set; } = true;
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
            get => _numberOfItems;
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
            get => _pause;
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
            if (Timer != null)
                Timer.Stop();

            if (_timerEnabled)
            {
                Timer.Interval = CarouselItems[ActiveIndex].Interval;
                Timer.Start();
            }
        }

        protected override void OnInitialized()
        {
            if (Interval == 0)
                _timerEnabled = false;

            if (Timer == null && _timerEnabled)
            {
                Timer = new Timer(Interval);
                Timer.Elapsed += OnTimerEvent;
                Timer.AutoReset = true;
                Timer.Start();
            }
        }

        private async Task DoAnimations()
        {
            if (CarouselItems.Count == 0) return;

            if (_timerEnabled)
            {
                Timer.Stop();
                Timer.Interval = CarouselItems[ActiveIndex].Interval;
            }
            if (Direction == 0)
            {
                var oldindex = ActiveIndex == 0 ? NumberOfItems - 1 : ActiveIndex - 1;
                new Task(async () =>
                {
                    AnimationRunning = true;
                    await CarouselItems[ActiveIndex].Clean().ConfigureAwait(false);
                    CarouselItems[ActiveIndex].Next = true;
                    await CarouselItems[ActiveIndex].StateChanged().ConfigureAwait(false);
                    await Task.Delay(300).ConfigureAwait(false);  // Gives animation time to shift and be ready to slide.
                    CarouselItems[ActiveIndex].Left = true;
                    CarouselItems[oldindex].Left = true;
                    await CarouselItems[ActiveIndex].StateChanged().ConfigureAwait(false); // makes sure there is no gap
                    await InvokeAsync(StateHasChanged).ConfigureAwait(false);
                }).Start();
            }
            else if (Direction == 1)
            {
                var oldindex = (ActiveIndex == NumberOfItems - 1) ? 0 : ActiveIndex + 1;
                new Task(async () =>
                {
                    AnimationRunning = true;
                    await CarouselItems[ActiveIndex].Clean().ConfigureAwait(false);
                    CarouselItems[ActiveIndex].Prev = true;
                    await CarouselItems[ActiveIndex].StateChanged().ConfigureAwait(false);
                    await Task.Delay(300).ConfigureAwait(false);  // Gives animation time to shift and be ready to slide.
                    CarouselItems[ActiveIndex].Right = true;
                    CarouselItems[oldindex].Right = true;
                    await CarouselItems[ActiveIndex].StateChanged().ConfigureAwait(false); // makes sure there is no gap
                    await InvokeAsync(StateHasChanged).ConfigureAwait(false);
                }).Start();
            }
        }

        public async Task AnimationEnd(BSCarouselItemBase sender)
        {
            if (sender == CarouselItems[ActiveIndex])
            {
                AnimationRunning = false;
                int oldindex = -1;
                if (Direction == 0)
                    oldindex = ActiveIndex == 0 ? NumberOfItems - 1 : ActiveIndex - 1;
                else if (Direction == 1)
                    oldindex = (ActiveIndex == NumberOfItems - 1) ? 0 : ActiveIndex + 1;

                await CarouselItems[oldindex].Clean().ConfigureAwait(false);
                await CarouselItems[oldindex].StateChanged().ConfigureAwait(false);
                await CarouselItems[ActiveIndex].Clean().ConfigureAwait(false);
                CarouselItems[ActiveIndex].Active = true;
                await InvokeAsync(StateHasChanged).ConfigureAwait(false);

                if (_timerEnabled)
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
            if (Interval != 0)
                _timerEnabled = true;

            if (Ride && ActiveIndex == 0)
            {
                if (_timerEnabled)
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
        }
    }
}
