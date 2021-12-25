using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSAccordion : BlazorStrapBase
    {
        [Parameter] public bool IsFlushed { get; set; }

        internal string? ClassBuilder => new CssBuilder("accordion")
            .AddClass("accordion-flush", IsFlushed)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        public bool FirstChild()
        {
            return ChildHandler == null;
        }

        public void Invoke(BSAccordionItem sender)
        {
            if(ChildHandler != null)
                ChildHandler(sender);
        }

        internal event Action<BSAccordionItem>? ChildHandler;
    }
}
