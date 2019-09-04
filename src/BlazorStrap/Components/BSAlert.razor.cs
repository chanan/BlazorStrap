using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public abstract class BSAlertBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        internal BSAlertEvent BSAlertEvent { get; set; }
        internal List<EventCallback<BSAlertEvent>> EventQue { get; set; } = new List<EventCallback<BSAlertEvent>>();
        protected string classname =>
       new CssBuilder().AddClass("alert")
           .AddClass(IsDismissible? "alert-dismissible" : "")
           .AddClass($"alert-{Color.ToDescriptionString()}")
           .AddClass(Class)
       .Build();

        [Parameter] public Color Color { get; set; } = Color.Primary;
        [Parameter] public bool IsDismissible { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter] public EventCallback<BSAlertEvent> CloseEvent { get; set; }
        [Parameter] public EventCallback<BSAlertEvent> ClosedEvent { get; set; }

        /// <summary>
        /// Gets or sets an action to be invoked when the alert is dismissed.
        /// </summary>
        [Parameter] public EventCallback OnDismiss { get; set; }

        protected bool IsOpen { get; set; } = true;

        protected void OnClick()
        {
            IsOpen = false;
            OnDismiss.InvokeAsync(EventCallback.Empty);
            BSAlertEvent = new BSAlertEvent() { Target = this };
            CloseEvent.InvokeAsync(BSAlertEvent);
            EventQue.Add(ClosedEvent);
        }
        protected override Task OnAfterRenderAsync(bool firstrun)
        {
            for (int i = 0; i < EventQue.Count; i++)
            {
                EventQue[i].InvokeAsync(BSAlertEvent);
                EventQue.RemoveAt(i);
            }

            return base.OnAfterRenderAsync(false);
        }
    }
}
