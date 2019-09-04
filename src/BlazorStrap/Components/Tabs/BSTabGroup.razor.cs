using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public abstract class BSTabGroupBase : ComponentBase
    {
        internal bool Disposing = false;
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        public List<BSTabBase> Tabs = new List<BSTabBase>();
        internal List<EventCallback<BSTabEvent>> EventQue { get; set; } = new List<EventCallback<BSTabEvent>>();
        internal BSTabEvent BSTabEvent { get; set; }
        private BSTabBase _selected;
        public BSTabBase Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                if (Disposing) return;
                BSTabEvent = new BSTabEvent() { Activated = value, Deactivated = _selected };

                ShowEvent.InvokeAsync(BSTabEvent);
                HideEvent.InvokeAsync(BSTabEvent);
                EventQue.Add(ShownEvent);
                EventQue.Add(HiddenEvent);
                _selected = value;
                InvokeAsync(StateHasChanged);
            }
        }

        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<BSTabEvent> ShowEvent { get; set; }
        [Parameter] public EventCallback<BSTabEvent> ShownEvent { get; set; }
        [Parameter] public EventCallback<BSTabEvent> HideEvent { get; set; }
        [Parameter] public EventCallback<BSTabEvent> HiddenEvent { get; set; }

        protected override Task OnAfterRenderAsync(bool firstrun)
        {
            for (int i = 0; i < EventQue.Count; i++)
            {
                EventQue[i].InvokeAsync(BSTabEvent);
                EventQue.RemoveAt(i);
            }

            return base.OnAfterRenderAsync(false);
        }
    }
}
