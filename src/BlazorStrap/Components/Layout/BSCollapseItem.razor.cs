using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public abstract class BSCollapseItemBase : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        internal BSCollapseBase Collapse { get; set; }

        private bool _active = false;
        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                if (value)
                {
                    Collapse?.Show();
                }
                StateHasChanged();
            }
        }
    }
}
