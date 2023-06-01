using Microsoft.AspNetCore.Components;
using System.Collections.Concurrent;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSAccordionBase : BlazorStrapBase
    {
        /// <summary>
        /// Adds the accordian-flush class. See 
        /// <see href="https://getbootstrap.com/docs/5.2/components/accordion/#flush">Bootstrap Documentation</see> 
        /// for details
        /// </summary>
        [Parameter] public bool IsFlushed { get; set; }
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
        internal ConcurrentDictionary<string, BSAccordionItemBase> Children = new();

        public bool UpdateChild(BSAccordionItemBase accordionItemBase)
        {
            return Children.TryGetValue(DataId, out var child) && Children.TryUpdate(DataId, accordionItemBase, child);
        }
        public void AddChild(BSAccordionItemBase child)
        {
            Children.TryAdd(child.DataId, child);
        }
        public void RemoveChild(BSAccordionItemBase child)
        {
            Children.TryRemove(child.DataId, out _);
        }
    }
}
