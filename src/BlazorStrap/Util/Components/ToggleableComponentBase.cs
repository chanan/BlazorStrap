using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorStrap.Util.Components
{
    /// <summary>
    /// The base class for Toggle BlazorStrap components.
    /// </summary>
    public abstract class ToggleableComponentBase : ComponentBase
    {
        internal ElementReference MyRef { get; set; }
        [Parameter] public EventCallback<bool> IsOpenChanged { get; set; }
        [Parameter] public bool DisableAnimations { get; set; }
        [Parameter] public string AnimationClass { get; set; }

        [Parameter]
        public bool? IsOpen
        {
            get => _isOpen;
            set
            {
                if (value != null)
                {
                    Manual = true;
                    if (value.Value != _isOpen)
                    {
                        Changed(value ?? false);
                        StateHasChanged();
                    }
                    _isOpen = value ?? false;
                }
            }
        }

        public bool Manual { get; set; } = false;
        private bool _isOpen { get; set; }
        internal bool InternalIsOpen { set => _isOpen = value; }

        internal virtual Task Changed(bool e)
        {
            return Task.FromResult(e);
        }
        public virtual void Show()
        {
            _isOpen = true;
            if (!Manual) Changed(_isOpen);
            IsOpenChanged.InvokeAsync(true);
        }
        public virtual void Hide()
        {
            _isOpen = false;
            if (!Manual) Changed(_isOpen);
            IsOpenChanged.InvokeAsync(false);
        }
        public virtual void Toggle()
        {
            _isOpen = !_isOpen;
            if (!Manual) Changed(_isOpen);
            IsOpenChanged.InvokeAsync(_isOpen);
        }
    }
}
