using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace BlazorStrap
{
    /// <summary>
    /// The base class for BlazorStrap components.
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
            _isOpen = true;
            JustOpened = true;
            IsOpenChanged.InvokeAsync(true);
            StateHasChanged();
        }
        public virtual void Hide()
        {
            _isOpen = false;
            IsOpenChanged.InvokeAsync(false);
            StateHasChanged();
        }
        public virtual void Toggle()
        {
            _isOpen = !_isOpen;
            JustOpened = _isOpen;
            IsOpenChanged.InvokeAsync(_isOpen);
            StateHasChanged();
        }
    }
}
