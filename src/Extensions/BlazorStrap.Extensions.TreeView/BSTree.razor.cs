using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace BlazorStrap.Extensions.TreeView
{
    public partial class BSTree : ComponentBase
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public bool IsExpanded { get; set; }
        [Parameter] public bool IsMultiSelect { get; set; }
        [Parameter] public bool IsDoubleClickToOpen { get; set; }
        [Parameter] public EventCallback<BSTreeItem> OnSelect { get; set; }
        [Parameter] public EventCallback<BSTreeItem> OnUnselect { get; set; }
        [Obsolete("Use OnSelect")]
        [Parameter] public EventCallback<BSTreeItem> ActiveItemAdded { get; set; }
        [Obsolete("Use OnUnselect")]
        [Parameter] public EventCallback<BSTreeItem> ActiveItemRemoved { get; set; }
        public List<BSTreeItem> ActiveTreeItem { get; set; } = new List<BSTreeItem>();

        public void AddActiveChild(BSTreeItem child)
        {
            if (ActiveTreeItem.Contains(child)) return;
            ActiveTreeItem.Add(child);

            if (ActiveItemAdded.HasDelegate)
            {
                ActiveItemAdded.InvokeAsync(child);
            }
            if (OnSelect.HasDelegate)
            {
                OnSelect.InvokeAsync(child);
            }
            StateHasChanged();
        }
        public void RemoveAllChildren()
        {
            var toRemove = new List<BSTreeItem>();
            foreach (var child in ActiveTreeItem)
            {
                if(child.IsAlwaysActive) continue;
                if (ActiveItemRemoved.HasDelegate)
                {
                    ActiveItemRemoved.InvokeAsync(child);
                }
                if (OnUnselect.HasDelegate)
                {
                    OnUnselect.InvokeAsync(child);
                }
                toRemove.Add(child);
            }
            foreach (var item in toRemove)
            {
                ActiveTreeItem.Remove(item);
            }
        }
        public void RemoveActiveChild(BSTreeItem child)
        {
            if(child.IsAlwaysActive) return;
            if (ActiveTreeItem.Contains(child))
            {
                ActiveTreeItem.Remove(child);
            }

            if (ActiveItemRemoved.HasDelegate)
            {
                ActiveItemRemoved.InvokeAsync(child);
            }
            if (OnUnselect.HasDelegate)
            {
                OnUnselect.InvokeAsync(child);
            }
            if (IsMultiSelect)
                StateHasChanged();
        }
    }
}
