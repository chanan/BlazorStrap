using Microsoft.AspNetCore.Components;


namespace BlazorStrap
{
    public abstract class BlazorStrapToggleBase<TValue> : BlazorStrapBase
    {
        internal bool CanRefresh { get; set; } = false;
        [Parameter] public EventCallback<TValue> OnShown { get; set; }
        [Parameter] public EventCallback<TValue> OnHidden { get; set; }
        [Parameter] public EventCallback<TValue> OnShow { get; set; }
        [Parameter] public EventCallback<TValue> OnHide { get; set; }
        
        /// <summary>
        /// Triggers a StateHasChanged Event on the component. 
        /// Importent! Use Only after OnShown is fired to avoid odd behaviour with transitions.
        /// </summary>
        /// <returns></returns>
        public virtual Task RefreshAsync()
        {
            return CanRefresh ? InvokeAsync(StateHasChanged) : Task.CompletedTask;
        }
        public virtual Task HideAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task ShowAsync()
        {
            return Task.CompletedTask;
        }
        public virtual Task ToggleAsync()
        {
            return Task.CompletedTask;
        }
        
        
    }
}