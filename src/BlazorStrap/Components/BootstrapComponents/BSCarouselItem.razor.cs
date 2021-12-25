using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSCarouselItem : BlazorStrapBase, IAsyncDisposable
    {
        [Parameter] public int Interval { get; set; } = 5000;
        private bool _active;
        [CascadingParameter] public BSCarousel? Parent { get; set; }

        private string? ClassBuilder => new CssBuilder("carousel-item")
            .AddClass("active", _active)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        internal ElementReference MyRef { get; set; }

        public void First()
        {
            _active = true;
            StateHasChanged();
        }

        public void Hide()
        {
            _active = false;
        }

        public void Show()
        {
            _active = true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Js.InvokeVoidAsync("blazorStrap.AddEvent", DataId, "bsCarouselItem", "transitionend");
                EventsSet = true;
            }
        }

        protected override void OnInitialized()
        {
            JSCallback.EventHandler += OnEventHandler;
            Parent?.AddChild(this);
        }

        protected override void OnParametersSet()
        {
            if (Interval < 1000 && Interval != 0)
            {
                throw new InvalidOperationException("BSCarouselItem can not have an Interval of less then 1000 and not 0");
            }
        }

        internal async Task TransitionEndAsync()
        {
            if (Parent != null) Parent.ClickLocked = false;
            await InvokeAsync(StateHasChanged);
        }

        private async void OnEventHandler(string id, string name, string type, Dictionary<string, string>? classList, JavascriptEvent? e)
        {
            if (DataId == id && name == "bsCarouselItem" && type == "transitionend")
            {
                await TransitionEndAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (EventsSet)
                await Js.InvokeVoidAsync("blazorStrap.RemoveEvent", DataId, "bsCarouselItem", "transitionend");
            JSCallback.EventHandler -= OnEventHandler;
            Parent?.RemoveChild(this);
            GC.SuppressFinalize(this);
        }
    }
}