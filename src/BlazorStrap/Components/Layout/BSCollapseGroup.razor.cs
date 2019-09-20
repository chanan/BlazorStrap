using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public abstract class BSCollapseGroupBase : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }

        private BSCollapseItem _selected;

        public BSCollapseItem Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                if (value == null)
                {
                    _selected?.Collapse.Hide();
                    _selected = null;
                }
                else
                {
                    _selected?.Collapse.Hide();
                    _selected = value;
                    _selected?.Collapse.Show();
                }
            }
        }
    }
}
