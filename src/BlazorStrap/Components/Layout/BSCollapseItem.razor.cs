using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSCollapseItem : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        internal BSCollapse Collapse { get; set; }

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
