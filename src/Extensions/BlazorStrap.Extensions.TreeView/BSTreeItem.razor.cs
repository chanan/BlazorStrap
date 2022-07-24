using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;

namespace BlazorStrap.Extensions.TreeView
{
    public partial class BSTreeItem : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object>? AdditionalAttributes { get; set; }
        [Parameter] public string? Id { get; set; }
        [Parameter] public RenderFragment? Label { get; set; }
        [Parameter] public string? TextLabel { get; set; }
        [Parameter] public string? Class { get; set; }
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public RenderFragment? Action { get; set; }
        private bool _active;
        [Parameter] public bool IsActive { get
            {
                if(Root != null)
                {
                    if (Root.ActiveTreeItem.Contains(this))
                        return true;
                }
                return _active;
            } 
            set => _active = value; 
        }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnDblClick { get; set; }
        [CascadingParameter] public BSTree? Root { get; set; }

        public BSTreeNode? Child { get; set; }
        [Parameter] public bool IsOpen {
            get { return _isOpen; }
            set { _isOpen = value; if(Child != null) Child.IsOpen = value; StateHasChanged(); } 
        }
        private bool _isOpen { get; set; }

        protected override void OnInitialized()
        {
            if (Root?.IsExpanded ?? false) 
            {
                IsOpen = true;
            }
        }

        private void DoActive(MouseEventArgs e)
        {
            if (Root == null) return;
            if (Root.IsMultiSelect && e.CtrlKey)
            {
                if (Root.ActiveTreeItem.Contains(this))
                    Root.RemoveActiveChild(this);
                else
                    Root.AddActiveChild(this);
            }
            else
            {
                Root.RemoveAllChildren();
                Root.AddActiveChild(this);
            }
        }
        protected void OnClickEvent(MouseEventArgs e)
        {
            if (Root == null) return;
            DoActive(e);
            if (!Root.IsDoubleClickToOpen)
            {
                IsOpen = !IsOpen;
                if (OnClick.HasDelegate)
                {
                    OnClick.InvokeAsync(e);
                }
            }
        }
        protected void OnDblClickEvent(MouseEventArgs e)
        {
            if (Root == null) return;
            if (Root.IsDoubleClickToOpen)
            {
                IsOpen = !IsOpen;

                if (OnClick.HasDelegate)
                {
                    OnClick.InvokeAsync(e);
                }
            }
        }
        private RenderFragment GetLabel()
        {
            if (TextLabel != null)
            {
                return new RenderFragment(builder => builder.AddContent(0, TextLabel));
            }
            return new RenderFragment(builder => builder.AddContent(0, Label));
        }
        public void ChildSet()
        {
            StateHasChanged();
        }
    }
}
