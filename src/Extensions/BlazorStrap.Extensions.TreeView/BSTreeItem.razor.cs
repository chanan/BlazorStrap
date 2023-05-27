using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStrap.Extensions.TreeView
{
    public partial class BSTreeItem : ComponentBase, IDisposable
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object>? AdditionalAttributes { get; set; }
        [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString();
        [Parameter] public RenderFragment? Label { get; set; }
        [Parameter] public string? TextLabel { get; set; }
        [Parameter] public string? Class { get; set; }
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public RenderFragment? Action { get; set; }
        [Parameter] public bool IsDefaultActive { get; set; }
        public bool IsActive { get; set; }
        [Parameter] public bool IsAlwaysActive { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnDblClick { get; set; }
        [CascadingParameter] public BSTree? Root { get; set; }
        private bool _defaultSet { get; set; }
        public BSTreeNode? Child { get; set; }
        
        [Parameter]
        public bool IsOpen
        {
            get { return _isOpen; }
            set { _isOpen = value; if (Child != null) Child.IsOpen = value; StateHasChanged(); }
        }
        private bool _isOpen { get; set; }
        protected async override Task OnParametersSetAsync()
        {
            if (IsAlwaysActive)
            {
                IsActive = true;
                if (!_defaultSet)
                {
                    await DoActiveAsync(new MouseEventArgs() { CtrlKey = true });
                }
                _defaultSet = true;
            }
            else if (IsDefaultActive && !_defaultSet)
            {
                _defaultSet = true;
                IsActive = true;
                await DoActiveAsync(new MouseEventArgs() { CtrlKey = true });
            }
            else if (Root != null)
            {
                IsActive = Root.ActiveTreeItem.Values.Contains(this);
            }
        }
        protected override void OnInitialized()
        {
            if(Root != null)
            {
                Root.OnRequest += OnRequestAsync;
            }
            if (Root?.IsExpanded ?? false)
            {
                IsOpen = true;
            }
        }

        private async Task OnRequestAsync(string type, string id)
        {
            if (Root == null) return;
            if(id != Id) return;
            if (type == "select")
            {
                if(!Root.IsMultiSelect)
                    await Root.ClearSelectionAsync();
                await Root.SelectAsync(this);
            }
            else
            {
                await Root.UnselectAsync(this);
            }
        }

        private async Task DoActiveAsync(MouseEventArgs e)
        {
            if (Root == null) return;
            if (Root.IsMultiSelect && e.CtrlKey)
            {
                if (Root.ActiveTreeItem.Values.Contains(this))
                    await Root.UnselectAsync(this);
                else
                    await Root.SelectAsync(this);
            }
            else
            {
                await Root.ClearSelectionAsync();
                await Root.SelectAsync(this);
            }
        }
        protected async Task OnClickEventAsync(MouseEventArgs e)
        {
            if (Root == null) return;
            await DoActiveAsync(e);
            if (!Root.IsDoubleClickToOpen)
            {
                IsOpen = !IsOpen;
                if (OnClick.HasDelegate)
                {
                    await OnClick.InvokeAsync(e);
                }
            }
        }
        protected async Task OnDblClickEventAsync(MouseEventArgs e)
        {
            if (Root == null) return;
            if (Root.IsDoubleClickToOpen)
            {
                IsOpen = !IsOpen;

                if (OnClick.HasDelegate)
                {
                    await OnClick.InvokeAsync(e);
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
   
        public async Task ChildSetAsync()
        {
            await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            if(Root != null)
                Root.OnRequest -= OnRequestAsync;
        }
    }
}