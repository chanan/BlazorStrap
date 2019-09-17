using Microsoft.AspNetCore.Components;
using System;

namespace BlazorStrap.Util.Components
{
    /// <summary>
    /// The base class for Toggle BlazorStrap components.
    /// </summary>
    public abstract class ToggleableComponentBase : ComponentBase
    {
        [Parameter] public EventCallback<bool> IsOpenChanged { get; set; }
 
        [Parameter]
        public bool? IsOpen
        {
            get
            {
                return _isOpen;
            }
            set
            {
                if (value != null)
                {
                    Manual = true;
                    Changed(value ?? false);
                    _isOpen = value ?? false;
                    StateHasChanged();
                }
            }
        }

        public bool Manual { get; set; } = false;
        private bool _isOpen { get; set; }

        internal virtual void Changed(bool e)
        {
        }
        public virtual void Show()
        {
            _isOpen = true;
            IsOpenChanged.InvokeAsync(true);
        }
        public virtual void Hide()
        {
            _isOpen = false;
            IsOpenChanged.InvokeAsync(false);
        }
        public virtual void Toggle()
        {
            _isOpen = !_isOpen;
            IsOpenChanged.InvokeAsync(_isOpen);
        }
    }
}
