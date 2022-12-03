using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSAccordionBase : BlazorStrapBase, IDisposable
    {
        /// <summary>
        /// Adds the accordian-flush class. See 
        /// <see href="https://getbootstrap.com/docs/5.2/components/accordion/#flush">Bootstrap Documentation</see> 
        /// for details
        /// </summary>
        [Parameter] public bool IsFlushed { get; set; }

        [CascadingParameter] public BSAccordionItemBase? Parent { get; set; }

        [CascadingParameter] public BSCollapseBase? CollapseParent { get; set; }

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }


        protected override void OnInitialized()
        {
            // if (Parent != null)
            //     Parent.NestedHandler += NestedHandler;
            // if (CollapseParent != null)
            //     CollapseParent.NestedHandler += NestedHandler;
        }

        // private void NestedHandler()
        // {
        //     ChildHandler?.Invoke(null);
        // }

        public bool FirstChild()
        {
            return ChildHandler == null;
        }

        public void Invoke(BSAccordionItemBase sender)
        {
            if (ChildHandler != null)
                ChildHandler(sender);

        }

        public void Dispose()
        {
            // if (Parent?.NestedHandler != null)
            //     Parent.NestedHandler -= NestedHandler;
            // if (CollapseParent != null)
            //     CollapseParent.NestedHandler -= NestedHandler;
        }


        internal Action<BSAccordionItemBase?>? ChildHandler;
    }
}
