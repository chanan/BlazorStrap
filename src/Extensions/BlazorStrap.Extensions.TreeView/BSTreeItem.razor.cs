using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace BlazorStrap.Extensions.TreeView
{
    public partial class BSTreeItem : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> AdditionalAttributes { get; set; }
        [Parameter] public bool IsActive { get; set; }
        [Parameter] public string Label { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public RenderFragment Action { get; set; }
        [Parameter] public bool Active { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnDblClick { get; set; }
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
            if (!Root.DoubleClickToOpen)
                IsOpen = !IsOpen;
            if (OnClick.HasDelegate)
            {
                OnClick.InvokeAsync(e);
            }
        }
        protected void OnDblClickEvent(MouseEventArgs e)
        {
            if (Root.DoubleClickToOpen)
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
