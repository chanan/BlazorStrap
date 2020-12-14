using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSCollapseGroup : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }

        private BSCollapseItem _selected;

        public BSCollapseItem Selected
        {
            get => _selected;
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
