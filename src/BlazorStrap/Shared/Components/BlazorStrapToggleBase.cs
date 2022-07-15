using Microsoft.AspNetCore.Components;


namespace BlazorStrap.Shared.Components
{
    public abstract class BlazorStrapToggleBase<TValue> : BlazorStrapBase
    {
        internal bool CanRefresh { get; set; } = false;

        /// <summary>
        /// Callback function fired when item is finished being shown.
        /// </summary>
        [Parameter] public EventCallback<TValue> OnShown { get; set; }

        /// <summary>
        /// Callback function fired when item is finished being hidden.
        /// </summary>
        [Parameter] public EventCallback<TValue> OnHidden { get; set; }

        /// <summary>
        /// Callback function fired immediately when item's show method is called.
        /// </summary>
        [Parameter] public EventCallback<TValue> OnShow { get; set; }

        /// <summary>
        /// Callback function fired immediately when item's hide method is called.
        /// </summary>
        [Parameter] public EventCallback<TValue> OnHide { get; set; }

        /// <summary>
        /// Whether or not element is shown.
        /// </summary>
        public abstract bool Shown { get; protected set; }

        /// <summary>
        /// Triggers a StateHasChanged Event on the component. 
        /// Important! Use Only after OnShown is fired to avoid odd behaviour with transitions.
        /// </summary>
        /// <returns></returns>
        /// 
        public virtual Task RefreshAsync()
        {
            return CanRefresh ? InvokeAsync(StateHasChanged) : Task.CompletedTask;
        }

        /// <summary>
        /// Hides the item.
        /// </summary>
        /// <returns>Completed task when hide is complete.</returns>
        public virtual Task HideAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Shows the item.
        /// </summary>
        /// <returns>Completed task when show is complete.</returns>
        public virtual Task ShowAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Toggles item open or closed
        /// </summary>
        /// <returns>Completed task when toggle is complete.</returns>
        public virtual Task ToggleAsync()
        {
            return Task.CompletedTask;
        }
    }
}