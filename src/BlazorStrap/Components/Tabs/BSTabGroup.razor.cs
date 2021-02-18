using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public partial class BSTabGroup : ComponentBase
    {
        internal string ReturnId { get; set; }
        internal bool Disposing { get; set; } = false;
        internal bool HasRendered { get; set; } = false;
        internal DynamicItem DynamicSelected { get; set; } = new DynamicItem();
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        public List<BSTab> Tabs { get; set; } = new List<BSTab>();
        internal List<EventCallback<BSTabEvent>> EventQue { get; set; } = new List<EventCallback<BSTabEvent>>();
        internal BSTabEvent BSTabEvent { get; set; }
        private BSTab _selected;
        public BSTab Selected
        {
            get => _selected;
            set
            {
                if (Disposing) return;
                BSTabEvent = new BSTabEvent() { Activated = value, Deactivated = _selected };

                if (HasRendered)
                {
                    InvokeAsync(() => ShowEvent.InvokeAsync(BSTabEvent));
                    InvokeAsync(() => HideEvent.InvokeAsync(BSTabEvent));
                    EventQue.Add(ShownEvent);
                    EventQue.Add(HiddenEvent);
                }
                _selected = value;
                InvokeAsync(StateHasChanged);
            }
        }
        internal BSTab InternalSelected
        {
            get => _selected;
            set
            {
                _selected = value;
            }
        }
        /// <summary>
        /// Activate a tab by Id
        /// </summary>
        /// <param name="tabId">The Id of the tab to select</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throws when the tab Id, passed in parameter <paramref name="tabId"/>,
        ///  does not exist
        /// </exception>
        public void SelectTabById(string tabId)
        {
            Selected = Tabs.Find(i => i.Id == tabId) ?? 
                throw new ArgumentOutOfRangeException($"The tab with Id { tabId } does not exist");
        }

        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<BSTabEvent> ShowEvent { get; set; }
        [Parameter] public EventCallback<BSTabEvent> ShownEvent { get; set; }
        [Parameter] public EventCallback<BSTabEvent> HideEvent { get; set; }
        [Parameter] public EventCallback<BSTabEvent> HiddenEvent { get; set; }

        public void UpdateDynamic()
        {
            _selected = null;
            StateHasChanged();
        }
        protected override Task OnAfterRenderAsync(bool firstrun)
        {
            if (firstrun)
            {
                HasRendered = true;
            }
            for (var i = 0; i < EventQue.Count; i++)
            {
                EventQue[i].InvokeAsync(BSTabEvent);
                EventQue.RemoveAt(i);
            }

            return base.OnAfterRenderAsync(false);
        }
    }
}
