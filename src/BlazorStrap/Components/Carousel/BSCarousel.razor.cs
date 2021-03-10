using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace BlazorStrap
{
    public partial class BSCarousel : ComponentBase, IDisposable
    {
        [CascadingParameter] public BSModal BSModal {get;set; }
    
        private const string _carouselFade = "carousel-fade";

        private int _activeIndex { get; set; }
        private int _prevIndex { get; set; }
        public int ActiveIndex
        {
            get => _activeIndex;
            set {
                _prevIndex = _activeIndex;
                _activeIndex = value;
            }
        }

        private bool _timerEnabled { get; set; } = true;
        public bool AnimationRunning { get; set; } = false;
        public List<BSCarouselIndicatorItem> CarouselIndicatorItems { get; set; }
        public List<BSCarouselItem> CarouselItems { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public bool Fade { get; set; } = false;
        [Parameter] public int Interval { get; set; } = 5000;
        [Parameter] public bool Keyboard { get; set; } = true;
        [Parameter] public int NumberOfItems { get; set; }
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
        public Timer TransitionTimer { get; set; }
        [Parameter] public bool Touch { get; set; } = true;
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Parameter] public bool Wrap { get; set; } = true;
        [Parameter] public EventCallback<int> ActiveIndexChangedEvent { get; set; }
        internal int Direction { get; set; }

        protected string Classname => new CssBuilder("carousel slide")
        .AddClass(_carouselFade, Fade)
        .AddClass(Class)
        .Build();

        private int _numberOfItems { get; set; }
        private string _pause { get; set; } = "hover";
        [Inject] protected Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }

        protected override void OnInitialized()
        {
            if (Interval == 0)
                _timerEnabled = false;

            if (Timer == null && _timerEnabled)
            {
                Timer = new Timer(Interval);
                Timer.Elapsed += OnTimerEvent;
                Timer.AutoReset = true;
                if (BSModal != null)
                {
                    BSModal.OnChanged += BSModal_OnChanged;
                    if (BSModal.IsOpen ?? false)
                        Timer.Start();
                }
                else
                {
                    Timer.Start();
                }
            }

            if (TransitionTimer == null)
            {
                TransitionTimer = new Timer(2000);
                TransitionTimer.Elapsed += OnTransitionTimerEvent;
                TransitionTimer.AutoReset = false;
            }
        }

        protected override void OnParametersSet()
        {

            if (CarouselItems == null)
            {
                CarouselItems = new List<BSCarouselItem>();
            }

            if (CarouselIndicatorItems == null)
            {
                CarouselIndicatorItems = new List<BSCarouselIndicatorItem>();
            }

            if (Interval != 0)
                _timerEnabled = true;

            if (Ride && ActiveIndex == 0)
            {
                if (_timerEnabled)
                    Timer.Start();
            }
        }

        public void Dispose()
        {
            if(BSModal != null)
            {
                BSModal.OnChanged -= BSModal_OnChanged;
            }
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

                if (TransitionTimer != null)
                {
                    TransitionTimer.Stop();
                    TransitionTimer.Dispose();
                }
            }
        }

        public async Task Refresh()
        {
            await InvokeAsync(StateHasChanged).ConfigureAwait(true);
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

        public void ResetTransitionTimer()
        {
            if (TransitionTimer != null)
            {
                TransitionTimer.Stop();
                TransitionTimer.Start();
            }
        }


        private void BSModal_OnChanged(bool e)
        {
            if (CarouselItems.Count == 0) return;

            AnimationRunning = false;
            if (Timer != null)
            {
                if (e == true)
                {
                    ResetTimer();
                }
                else
                {
                    Timer.Stop();
                }
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

            AnimationRunning = true;
            CarouselItems[ActiveIndex].Clean();

            Direction = GetDirection();

            //Add new item to DOM on appropriate side
            CarouselItems[ActiveIndex].Next = Direction == 0;
            CarouselItems[ActiveIndex].Prev = Direction == 1;
            this.StateHasChanged();
            await Task.Delay(300).ConfigureAwait(true); //Ensure new item is rendered on DOM before continuing

            //Trigger Animation
            CarouselItems[ActiveIndex].Left = Direction == 0;
            CarouselItems[_prevIndex].Left = Direction == 0;

            CarouselItems[ActiveIndex].Right = Direction == 1;
            CarouselItems[_prevIndex].Right = Direction == 1;
            this.StateHasChanged();

            ResetTransitionTimer();
        }

        public async Task AnimationEnd(BSCarouselItem sender)
        {
            if (sender == CarouselItems[ActiveIndex])
            {
                AnimationRunning = false;

                CarouselItems[_prevIndex].Clean();
                CarouselItems[ActiveIndex].Clean();
                CarouselItems[ActiveIndex].Active = true;
                this.StateHasChanged();

                if (_timerEnabled)
                    Timer.Start();

                await ActiveIndexChangedEvent.InvokeAsync(ActiveIndex).ConfigureAwait(false);
            }
        }

        protected async Task OnKeyPress(KeyboardEventArgs e)
        {
            if (!Keyboard) return;
            if (e?.Code == "37")
            {
                await GoToPrevItem().ConfigureAwait(true);
            }
            else if (e?.Code == "39")
            {
                await GoToNextItem().ConfigureAwait(true);
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
        
        public async Task GoToSpecificItem(int index)
        {
            if (AnimationRunning) return;
            ResetTimer();
            ActiveIndex = index;
            await DoAnimations().ConfigureAwait(true);
        }

        public async Task GoToNextItem()
        {
            if (AnimationRunning) return;
            ResetTimer();
            ActiveIndex = ActiveIndex == NumberOfItems - 1 ? 0 : ActiveIndex + 1;
            await DoAnimations().ConfigureAwait(true);
        }

        public async Task GoToPrevItem()
        {
            if (AnimationRunning) return;
            ResetTimer();
            ActiveIndex = ActiveIndex == 0 ? NumberOfItems - 1 : ActiveIndex - 1;
            await DoAnimations().ConfigureAwait(true);
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
            await InvokeAsync(DoAnimations).ConfigureAwait(true);
            await OnSlide.InvokeAsync(null).ConfigureAwait(true);
        }

        private async void OnTransitionTimerEvent(object source, ElapsedEventArgs e)
        {
            if (!AnimationRunning) return;

            // If the active index is not "active" by the time the timer is elapsed something is very wrong, reset the animation
            if (!CarouselItems[ActiveIndex].Active)
            {
                await InvokeAsync(async () => {
                    await AnimationEnd(CarouselItems[ActiveIndex]).ConfigureAwait(true);
                    return;
                }).ConfigureAwait(true);
            }
        }

        private int GetDirection()
        {
            if (_prevIndex == 0)
            {
                if (ActiveIndex == NumberOfItems - 1)
                {
                    return 1;
                } else
                {
                    return 0;
                }
            }

            if (_prevIndex == NumberOfItems - 1)
            {
                if (ActiveIndex == 0)
                {
                    return 0;
                } else
                {
                    return 1;
                }
            }

            if (ActiveIndex > _prevIndex)
            {
                return 0;
            } else
            {
                return 1;
            }
        }

    }
}
