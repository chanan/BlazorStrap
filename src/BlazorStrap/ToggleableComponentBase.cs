using Microsoft.AspNetCore.Components;
using System;

namespace BlazorStrap
{
    /// <summary>
    /// The base class for Toggle BlazorStrap components.
    /// </summary>
    public abstract class ToggleableComponentBase : BootstrapComponentBase
    {
        [Parameter] protected EventCallback<bool> IsOpenChanged { get; set; }
        [Parameter]
        protected bool? IsOpen
        {
            get
            {
                return _isOpen;
            }
            set
            {
                if(value != null)
                {
                    _manual = true;
                }
                if (value != _isOpen)
                {
                    _isOpen = value.HasValue ? value.Value : false;
                    IsOpenChanged.InvokeAsync(_isOpen);
                }
                if (value == true)
                {
                    JustOpened = true;
                }
            }
        }

        protected bool _isOpen { get; set; }
        protected bool _manual { get; set; }
        protected bool JustOpened { get; set; }
        public virtual void Show()
        {
            if (_manual)
            {
                throw new InvalidOperationException("Boolean IsOpen and @Ref show/hide/toggle. May not be used together. ");
            }
            _isOpen = true;
            JustOpened = true;
            IsOpenChanged.InvokeAsync(true);
            StateHasChanged();
        }
        public virtual void Hide()
        {
            if (_manual)
            {
                throw new InvalidOperationException("Boolean IsOpen and @Ref show/hide/toggle. May not be used together. ");
            }
            _isOpen = false;
            IsOpenChanged.InvokeAsync(false);
            StateHasChanged();
        }
        public virtual void Toggle()
        {
            if (_manual)
            {
                throw new InvalidOperationException("Boolean IsOpen and @Ref show/hide/toggle. May not be used together. ");
            }
            _isOpen = !_isOpen;
            JustOpened = _isOpen;
            IsOpenChanged.InvokeAsync(_isOpen);
            StateHasChanged();
        }
    }
}
