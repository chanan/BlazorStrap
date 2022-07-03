using BlazorStrap.Bootstrap.Interfaces;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Bootstrap.Base
{
    internal class AlertBase : LayoutBase, IAlert
    {
        [Parameter] public BSColor Color { get; set; }
        [Parameter] public RenderFragment? Content { get; set; }
        [Parameter] public EventCallback Dismissed { get; set; }
        [Parameter] public bool HasIcon { get; set; }
        [Parameter] public RenderFragment? Header { get; set; }
        [Parameter] public int Heading { get; set; }
        [Parameter] public bool IsDismissible { get; set; }


        protected bool IsDismissed;
        
        public virtual async Task CloseEventAsync()
        {
            await EventUtil.AsNonRenderingEventHandler(Dismissed.InvokeAsync).Invoke();
            IsDismissed = true;
        }

        public virtual void Open()
        {
            IsDismissed = false;
        }
    }
}