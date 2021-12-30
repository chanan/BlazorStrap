using Microsoft.AspNetCore.Components;


namespace BlazorStrap
{
    public abstract class BlazorStrapToggleBase<TValue> : BlazorStrapBase
    {
        [Parameter] public EventCallback<TValue> OnShown { get; set; }
        [Parameter] public EventCallback<TValue> OnHidden { get; set; }
        [Parameter] public EventCallback<TValue> OnShow { get; set; }
        [Parameter] public EventCallback<TValue> OnHide { get; set; }
        
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