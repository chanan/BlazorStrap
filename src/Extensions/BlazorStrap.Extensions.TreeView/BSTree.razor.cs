using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorStrap.Extensions.TreeView
{
    public partial class BSTree : ComponentBase
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public bool IsExpanded { get; set; }
        [Parameter] public bool IsMultiSelect { get; set; }
        [Parameter] public bool IsDoubleClickToOpen { get; set; }
        [Parameter] public EventCallback<BSTreeItem> ActiveItemAdded { get; set; }
        [Parameter] public EventCallback<BSTreeItem> ActiveItemRemoved { get; set; }
        public List<BSTreeItem> ActiveTreeItem { get; set; } = new List<BSTreeItem>();

        public void AddActiveChild(BSTreeItem child)
        {
            if (ActiveTreeItem.Contains(child)) return;
            ActiveTreeItem.Add(child);
            if(ActiveItemAdded.HasDelegate)
            {
                ActiveItemAdded.InvokeAsync(child);
            }
            StateHasChanged();
        }
        public void RemoveAllChildren()
        {
            foreach(var child in ActiveTreeItem)
            {
                if (ActiveItemRemoved.HasDelegate)
                {
                    ActiveItemRemoved.InvokeAsync(child);
                }
            }
            ActiveTreeItem.Clear();
        }
        public void RemoveActiveChild(BSTreeItem child)
        {
            if (ActiveTreeItem.Contains(child))
            {
                ActiveTreeItem.Remove(child);
            }
            if (ActiveItemRemoved.HasDelegate)
            {
                ActiveItemRemoved.InvokeAsync(child);
            }
            if (IsMultiSelect)
                StateHasChanged();
        }
    }
}
