using Microsoft.AspNetCore.Components;
using System;

namespace BlazorStrap.Util.Components
{
    /// <summary>
    /// The base class for Toggle BlazorStrap components.
    /// </summary>
    public abstract class ToggleableComponentBase : ComponentBase
    {
        [Parameter] protected EventCallback<bool> IsOpenChanged { get; set; }
 
        [Parameter]
        protected bool? IsOpen
        {
            get
            {
                return _manual;
            }
            set
            {
                if (value != _manual)
                {
                    Changed(value.Value);
                    StateHasChanged();
                }
                if (value != null)
                {
                    _manual = value;
                }
               
                if (value == true)
                {
                    if (JustOpened)
                    {
                        JustOpened = false;
                    }
                    else
                    {
                        JustOpened = true;
                    }
                }
            }
        }

        protected bool _isOpen { get; set; }
       
        protected bool? _manual { get; set; }
        protected bool JustOpened { get; set; }

        internal virtual void Changed(bool e)
        {
        }
        public virtual void Show()
        {
            if (_manual != null)
            {
                throw new InvalidOperationException("Boolean IsOpen and @Ref show/hide/toggle. May not be used together. ");
            }
            _isOpen = true;
            JustOpened = true;
            Changed(true);
            IsOpenChanged.InvokeAsync(true);
            StateHasChanged();
        }
        public virtual void Hide()
        {
            if (_manual != null )
            {
                throw new InvalidOperationException("Boolean IsOpen and @Ref show/hide/toggle. May not be used together. ");
            }
            _isOpen = false;
            Changed(false);
            IsOpenChanged.InvokeAsync(false);
            StateHasChanged();
        }
        public virtual void Toggle()
        {
            if (_manual != null)
            {
                throw new InvalidOperationException("Boolean IsOpen and @Ref show/hide/toggle. May not be used together. ");
            }
            _isOpen = !_isOpen;
            JustOpened = _isOpen;
            Changed(_isOpen);
            IsOpenChanged.InvokeAsync(_isOpen);
            StateHasChanged();
        }

      
    }
}
