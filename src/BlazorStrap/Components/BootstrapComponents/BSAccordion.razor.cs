using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSAccordion : BlazorStrapBase, IDisposable
    {
        
        [Parameter] public bool IsFlushed { get; set; }
        
        [CascadingParameter] public BSAccordionItem Parent { get; set; }
        

        private string? ClassBuilder => new CssBuilder("accordion")
            .AddClass("accordion-flush", IsFlushed)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        protected override void OnInitialized()
        {
            if(Parent != null)
                Parent.ParentHandler += ParentHandler;
        }

        private void ParentHandler()
        {
            ChildHandler?.Invoke(null);
        }

        public bool FirstChild()
        {
            return ChildHandler == null;
        }

        public void Invoke(BSAccordionItem sender)
        {
            if(ChildHandler != null)
                ChildHandler(sender);
            
        }

        public void Dispose()
        {
            if (Parent != null)
            {
                Parent.ParentHandler -= ParentHandler;
            }
        }


        internal Action<BSAccordionItem?>? ChildHandler;
        
    }
}
