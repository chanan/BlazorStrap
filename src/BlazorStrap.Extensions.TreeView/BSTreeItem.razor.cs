using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap.Extensions.TreeView
{
    public partial class BSTreeItem : ComponentBase
    {
        [Parameter] public bool IsActive { get; set; }
        [Parameter] public string Label { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public RenderFragment Action { get; set; }
        [Parameter] public bool Active { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [CascadingParameter] public BSTree Root { get; set; }
        public BSTreeNode Child { get; set; }
        [Parameter] public bool IsOpen {
            get { return _isOpen; }
            set { _isOpen = value; if(Child != null) Child.IsOpen = value; StateHasChanged(); } 
        }
        private bool _isOpen { get; set; }

        protected override void OnInitialized()
        {
            if (Root.Expand)
            {
                IsOpen = true;
            }
        }
        protected void OnClickEvent(MouseEventArgs e)
        {
            IsOpen = !IsOpen;
            if (OnClick.HasDelegate)
            {
                OnClick.InvokeAsync(e);
            }
        }
        public void ChildSet()
        {
            StateHasChanged();
        }
    }
}
