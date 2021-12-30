using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSCarouselItem : BlazorStrapToggleBase<BSCarouselItem>, IDisposable
    {
        [Parameter] public int Interval { get; set; } = 5000;
        private bool _active;
        private bool _once;
        [CascadingParameter] public BSCarousel? Parent { get; set; }
        private string? ClassBuilder => new CssBuilder("carousel-item")
            .AddClass("active", _active)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        internal ElementReference MyRef { get; private set; }

        public void First()
        {
            _active = true;
            StateHasChanged();
        }

        public override async Task HideAsync()
        {
            if (Parent == null) return;
            if (OnHide.HasDelegate)
                await OnHide.InvokeAsync(this);
            await Parent.HideSlide(this);
        }
        
        public override async Task ShowAsync()
        {
            if (Parent == null) return;
            if (OnShow.HasDelegate)
                await OnShow.InvokeAsync(this);
            await Parent.GotoChildSlide(this);
        }
        public override Task ToggleAsync()
        {
            return (_active) ? HideAsync() : ShowAsync();
        }

        internal async Task InternalHide()
        {
            
            if (OnHide.HasDelegate)
                _ = OnHide.InvokeAsync(this);
            _active = false;
        }

        internal async Task InternalShow()
        {
            if (OnShow.HasDelegate)
                _ = OnShow.InvokeAsync(this);
            _active = true;
        }
        
        internal Task Refresh()
        {
            return InvokeAsync(StateHasChanged);
        }
        protected override void OnInitialized()
        {
            
            Parent?.AddChild(this);
        }

        protected override void OnParametersSet()
        {
            if (Interval < 1000 && Interval != 0)
            {
                throw new InvalidOperationException("BSCarouselItem can not have an Interval of less then 1000 and not 0");
            }
        }


        public void Dispose()
        {
            Parent?.RemoveChild(this);
        }
    }
}