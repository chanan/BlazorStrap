using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public ConcurrentDictionary<string, BSTreeItem> ActiveTreeItem { get; set; } = new();
        public Func<string,string, Task>? OnRequest { get; set; }

        public async Task SelectAsync(string id)
        {
            if(OnRequest != null)
                await OnRequest.Invoke("select", id);
        }
        public async Task UnselectAsync(string id)
        {
            if (OnRequest != null)
                await OnRequest.Invoke("unselect", id);
        }

        public async Task SelectAsync(BSTreeItem child)
        {
            if (ActiveTreeItem.Values.Contains(child)) return;
            if (ActiveTreeItem.TryAdd(child.Id, child))
            {
                if (ActiveItemAdded.HasDelegate)
                {
                    await ActiveItemAdded.InvokeAsync(child);
                }
                if (OnSelect.HasDelegate)
                {
                    await OnSelect.InvokeAsync(child);
                }
            }
            await InvokeAsync(StateHasChanged);
        }

        public async Task ClearSelectionAsync()
        {
            var toRemove = new List<BSTreeItem>();
            foreach (var child in ActiveTreeItem.Values)
            {
                if (child.IsAlwaysActive) continue;
                toRemove.Add(child);
            }
            foreach (var item in toRemove)
            {
                if(ActiveTreeItem.TryRemove(item.Id, out _))
                {
                    if (ActiveItemRemoved.HasDelegate)
                    {
                        await ActiveItemRemoved.InvokeAsync(item);
                    }
                    if (OnUnselect.HasDelegate)
                    {
                        await OnUnselect.InvokeAsync(item);
                    }
                }
            }
            await InvokeAsync(StateHasChanged);
        }

       
        public async Task UnselectAsync(BSTreeItem child)
        {
            if (child.IsAlwaysActive) return;
            if (ActiveTreeItem.Values.Contains(child))
            {
                if (ActiveTreeItem.TryRemove(child.Id, out _))
                {
                    if (ActiveItemRemoved.HasDelegate)
                    {
                        await ActiveItemRemoved.InvokeAsync(child);
                    }
                    if (OnUnselect.HasDelegate)
                    {
                        await OnUnselect.InvokeAsync(child);
                    }
                }
            }
            await InvokeAsync(StateHasChanged);
        }

        [Obsolete("Use AddActiveChildAsync")]
        public void AddActiveChild(BSTreeItem child)
        {
           _ = SelectAsync(child);
        }

        [Obsolete("Use RemoveActiveChildAsync")]
        public void RemoveActiveChild(BSTreeItem child)
        {
           _ = UnselectAsync(child);
        }

        [Obsolete("Use RemoveAllChildrenAsync")]
        public void RemoveAllChildren()
        {
            _ = ClearSelectionAsync();
        }

    }
}
