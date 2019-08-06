using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public class CodeBSAlert : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        internal BSAlertEvent BSAlertEvent { get; set; }
        internal List<EventCallback<BSAlertEvent>> EventQue { get; set; } = new List<EventCallback<BSAlertEvent>>();
        protected string classname =>
       new CssBuilder().AddClass("alert")
           .AddClass($"alert-{Color.ToDescriptionString()}")
           .AddClass(Class)
       .Build();

        [Parameter] protected Color Color { get; set; } = Color.Primary;
        [Parameter] protected bool IsDismissible { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }

        [Parameter] protected EventCallback<BSAlertEvent> CloseEvent { get; set; }
        [Parameter] protected EventCallback<BSAlertEvent> ClosedEvent { get; set; }

        /// <summary>
        /// Gets or sets an action to be invoked when the alert is dismissed.
        /// </summary>
        [Parameter] protected EventCallback OnDismiss { get; set; }

        protected bool IsOpen { get; set; } = true;

        protected void OnClick()
        {
            IsOpen = false;
            OnDismiss.InvokeAsync(EventCallback.Empty);
            BSAlertEvent = new BSAlertEvent() { Target = this };
            CloseEvent.InvokeAsync(BSAlertEvent);
            EventQue.Add(ClosedEvent);
        }
        protected override Task OnAfterRenderAsync()
        {
            for (int i = 0; i < EventQue.Count; i++)
            {
                EventQue[i].InvokeAsync(BSAlertEvent);
                EventQue.RemoveAt(i);
            }

            return base.OnAfterRenderAsync();
        }
    }
}
