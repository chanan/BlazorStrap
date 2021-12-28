using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Timers;
namespace BlazorStrap
{
    public partial class BSCarousel : BlazorStrapBase, IDisposable
    {
        [Parameter] public bool HasControls { get; set; }
        [Parameter] public bool HasIndicators { get; set; }
        [Parameter] public bool IsDark { get; set; }
        [Parameter] public bool IsFade { get; set; }
        [Parameter] public bool IsSlide { get; set; } = true;

        private int _active;
        private InternalComponents.Indicators? _indicatorsRef;
        private int _last;
        private System.Timers.Timer? _transitionTimer;
        public bool ClickLocked { get; set; }
        private List<BSCarouselItem> Children { get; set; } = new List<BSCarouselItem>();

        private string? ClassBuilder => new CssBuilder("carousel")
            .AddClass("slide", IsSlide)
            .AddClass("carousel-fade", IsFade)
            .AddClass("carousel-dark", IsDark)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();


        public async Task GotoSlide(int slide)
        {
            var back = slide < _active;
            if(slide == _active) return;
            _last = _active;
            _active = slide;
            Children[_last].InternalHide();
            Children[_active].InternalShow();
            if (Js != null)
            {
                await Js.InvokeVoidAsync("blazorStrap.AnimateCarousel", Children[_active].MyRef, Children[_last].MyRef, back);
                if (await Js.InvokeAsync<bool>("blazorStrap.TransitionDidNotStart", Children[_active].MyRef))
                {
                    await Children[_active].TransitionEndAsync();
                    await Children[_last].TransitionEndAsync();
                }
            }

            await InvokeAsync(() =>
            {
                _indicatorsRef?.Refresh(Children.Count, _active);
            });
        }

        internal Task HideSlide(BSCarouselItem slide)
        {
            return GotoChildSlide(slide == Children.First() ? Children.Last() : Children.First());
        }

        internal async Task GotoChildSlide(BSCarouselItem item)
        {
            var slide = Children.IndexOf(item);
            var back = slide < _active;
            if(slide == _active) return;
            _last = _active;
            _active = slide;
            Children[_last].InternalHide();
            Children[_active].InternalShow();
            if (Js != null)
            {
                await Js.InvokeVoidAsync("blazorStrap.AnimateCarousel", Children[_active].MyRef, Children[_last].MyRef, back);
                if (await Js.InvokeAsync<bool>("blazorStrap.TransitionDidNotStart", Children[_active].MyRef))
                {
                    await Children[_active].TransitionEndAsync();
                    await Children[_last].TransitionEndAsync();
                }
            }

            await InvokeAsync(() =>
            {
                _indicatorsRef?.Refresh(Children.Count, _active);
            });
        }

        internal async Task BackAsync()
        {
            if(ClickLocked) return;
            ClickLocked = true;
            var last = _active;
            _active--;
            
            if (_active < 0)
                _active = Children.Count - 1;
            if (last == 0)
                _last = last;

            else
                _last = _active + 1;
            Children[_last].InternalHide();
            Children[_active].InternalShow();
            if (Js != null)
            {
                await Js.InvokeVoidAsync("blazorStrap.AnimateCarousel", Children[_active].MyRef, Children[_last].MyRef, true);
                if (await Js.InvokeAsync<bool>("blazorStrap.TransitionDidNotStart", Children[_active].MyRef))
                {
                    await Children[_active].TransitionEndAsync();
                    await Children[_last].TransitionEndAsync();
                }
            }

            await InvokeAsync(() =>
            {
                _indicatorsRef?.Refresh(Children.Count, _active);
            });
            ResetTransitionTimer(Children[_active].Interval);
        }

        internal async Task NextAsync()
        {
            if(ClickLocked) return;
            ClickLocked = true;
            _active++;
            if (_active > Children.Count - 1)
                _active = 0;
            
            if (_active == 0)
                _last = Children.Count - 1;

            else
                _last = _active - 1;
            Children[_last].InternalHide();
            Children[_active].InternalShow();
            if (Js != null)
            {
                await Js.InvokeVoidAsync("blazorStrap.AnimateCarousel", Children[_active].MyRef, Children[_last].MyRef, false);
                if (await Js.InvokeAsync<bool>("blazorStrap.TransitionDidNotStart", Children[_active].MyRef))
                {
                    await Children[_active].TransitionEndAsync();
                    await Children[_last].TransitionEndAsync();
                }
            }

            await InvokeAsync(() =>
            {
                _indicatorsRef?.Refresh(Children.Count, _active);
            });
            ResetTransitionTimer(Children[_active].Interval);
        }

        internal void AddChild(BSCarouselItem item)
        {
            Children.Add(item);
            if (Children.First() == item)
            {
                item.First();
                if (item.Interval != 0)
                {
                    InitializeTransitionTimer(item.Interval);
                    _transitionTimer?.Start();
                }
            }

            _indicatorsRef?.Refresh(Children.Count, _active);
        }

        internal void RemoveChild(BSCarouselItem item)
        {
            Children.Remove(item);
            _indicatorsRef?.Refresh(Children.Count,_active);
        }

        private async Task PressEvent(KeyboardEventArgs e)
        {
            if (e.Code == "37")
            {
                await BackAsync();
            }
            else if (e.Code == "39")
            {
                await NextAsync();
            }
        }

        private void InitializeTransitionTimer(int interval)
        {
            _transitionTimer = new System.Timers.Timer(interval);
            _transitionTimer.Elapsed += OnTransitionTimerEvent;
            _transitionTimer.AutoReset = false;
        }

        private async void OnTransitionTimerEvent(object sender, ElapsedEventArgs e)
        {
            await NextAsync();
        }

        private void ResetTransitionTimer(int interval)
        {
            _transitionTimer?.Stop();
            if (interval == 0) return;
            InitializeTransitionTimer(interval); // Avoid an System.ObjectDisposedException due to the timer being disposed. This occurs when the Enabled property of the timer is set to false by the call to Stop() above.
            _transitionTimer?.Start(); 
        }

        public void Dispose()
        {
            _transitionTimer?.Stop();
            _transitionTimer?.Dispose();
        }
    }
}