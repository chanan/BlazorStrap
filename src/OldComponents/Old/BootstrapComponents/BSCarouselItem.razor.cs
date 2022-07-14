using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSCarouselItem : BlazorStrapToggleBase<BSCarouselItem>, IDisposable
    {
        /// <summary>
        /// <para>
        /// Sets the amount of time to delay (in milliseconds) between automatically cycling to the next item.
        /// </para>
        /// <para>
        /// Defaults to 5000ms if not set. Valid values are greater than 1000 ms. A value of 0 disables automatic cycling.
        /// </para>
        /// </summary>
        /// <remarks>
        /// An <see cref="InvalidOperationException"/> will be thrown if value is between 0 and 1000 ms.
        /// </remarks>
        [Parameter] public int Interval { get; set; } = 5000;

        private bool _active;
        [CascadingParameter] public BSCarousel? Parent { get; set; }
        private string? ClassBuilder => new CssBuilder("carousel-item")
            .AddClass("active", _active)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        internal ElementReference? MyRef { get; private set; }

        public void First()
        {
            _active = true;
            StateHasChanged();
        }

        /// <inheritdoc/>
        public override async Task HideAsync()
        {
            if (Parent == null) return;
            CanRefresh = false;
            if (OnHide.HasDelegate)
                await OnHide.InvokeAsync(this);
            await Parent.HideSlide(this);
        }

        /// <inheritdoc/>
        public override async Task ShowAsync()
        {
            if (Parent == null) return;
            CanRefresh = false;
            if (OnShow.HasDelegate)
                await OnShow.InvokeAsync(this);
            await Parent.GotoChildSlide(this);
        }

        /// <inheritdoc/>
        public override Task ToggleAsync()
        {
            return (_active) ? HideAsync() : ShowAsync();
        }

        internal Task InternalHide()
        {
            CanRefresh = false;
            if (OnHide.HasDelegate)
                _ = OnHide.InvokeAsync(this);
            _active = false;
            return Task.CompletedTask;
        }

        internal Task InternalShow()
        {
            CanRefresh = false;
            if (OnShow.HasDelegate)
                _ = OnShow.InvokeAsync(this);
            _active = true;
            return Task.CompletedTask;
        }

        internal Task Refresh()
        {
            CanRefresh = true;
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