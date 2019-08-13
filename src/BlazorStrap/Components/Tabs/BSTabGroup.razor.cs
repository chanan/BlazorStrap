using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public class CodeBSTabGroup : ComponentBase
    {
        internal bool Disposing = false;
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        public List<CodeBSTab> Tabs = new List<CodeBSTab>();
        internal List<EventCallback<BSTabEvent>> EventQue { get; set; } = new List<EventCallback<BSTabEvent>>();
        internal BSTabEvent BSTabEvent { get; set; }
        private CodeBSTab _selected;
        public CodeBSTab Selected
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
                StateHasChanged();
            }
        }

        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<BSTabEvent> ShowEvent { get; set; }
        [Parameter] public EventCallback<BSTabEvent> ShownEvent { get; set; }
        [Parameter] public EventCallback<BSTabEvent> HideEvent { get; set; }
        [Parameter] public EventCallback<BSTabEvent> HiddenEvent { get; set; }

        protected override Task OnAfterRenderAsync()
        {
            for (int i = 0; i < EventQue.Count; i++)
            {
                EventQue[i].InvokeAsync(BSTabEvent);
                EventQue.RemoveAt(i);
            }

            return base.OnAfterRenderAsync();
        }
    }
}
