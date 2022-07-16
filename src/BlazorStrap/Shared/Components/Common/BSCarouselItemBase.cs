using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSCarouselItemBase : BlazorStrapToggleBase<BSCarouselItemBase>, IDisposable
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

        protected bool Active;
        [CascadingParameter] public BSCarouselBase? Parent { get; set; }
        public override bool Shown { get => Active; protected set { } }

        public ElementReference? MyRef { get; protected set; }
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }

        public void First()
        {
            Active = true;
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
            return (Active) ? HideAsync() : ShowAsync();
        }

        internal Task InternalHide()
        {
            CanRefresh = false;
            if (OnHide.HasDelegate)
                _ = OnHide.InvokeAsync(this);
            Active = false;
            return Task.CompletedTask;
        }

        internal Task InternalShow()
        {
            CanRefresh = false;
            if (OnShow.HasDelegate)
                _ = OnShow.InvokeAsync(this);
            Active = true;
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
