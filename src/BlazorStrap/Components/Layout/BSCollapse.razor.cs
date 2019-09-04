using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSCollapseBase : ToggleableComponentBase 
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Parameter] public EventCallback<BSCollapseEvent> ShowEvent { get; set; }
        [Parameter] public EventCallback<BSCollapseEvent> ShownEvent { get; set; }
        [Parameter] public EventCallback<BSCollapseEvent> HideEvent { get; set; }
        [Parameter] public EventCallback<BSCollapseEvent> HiddenEvent { get; set; }
        [Parameter] public bool IsList { get; set; }
        [CascadingParameter] protected BSNavbar Navbar { get; set; }
        internal BSCollapseEvent BSCollapseEvent { get; set; }
        internal List<EventCallback<BSCollapseEvent>> EventQue { get; set; } = new List<EventCallback<BSCollapseEvent>>();

        [CascadingParameter] internal BSCollapseItem CollapseItem { get; set; }
        protected string Tag => IsList ? "li" : "div";
        protected string classname =>
         new CssBuilder("collapse")
             .AddClass("navbar-collapse", IsNavbar)
             .AddClass("show", IsOpen.HasValue && IsOpen.Value || _isOpen)
             .AddClass(Class)
         .Build();

        [Parameter] public bool IsNavbar { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    

        protected override void OnInitialized()
        {
            if(IsNavbar && Navbar != null)
            {
                Navbar.VisableChange += OnVisableChange;
            }
            if(CollapseItem != null)
            {
                CollapseItem.Collapse = this;
            }
        }

        private void OnVisableChange(object sender, bool e)
        {
            _isOpen = e;
            InvokeAsync(() => { IsOpenChanged.InvokeAsync(e); });
            
        }

        internal override void Changed(bool e)
        {
            BSCollapseEvent = new BSCollapseEvent() { Target = this };
            if(e)
            {
                ShowEvent.InvokeAsync(BSCollapseEvent);
                EventQue.Add(ShownEvent);
            }
            else
            {
                if (IsNavbar && Navbar != null)
                {
                    if (Navbar.HasCollapsed == false)
                    {
                        Navbar.HasCollapsed = true;
                        Navbar.Visable = false;
                    }
                }
                HideEvent.InvokeAsync(BSCollapseEvent);
                EventQue.Add(HiddenEvent);
            }
        }

        protected override Task OnAfterRenderAsync()
        {
            for (int i = 0; i < EventQue.Count; i++)
            {
                EventQue[i].InvokeAsync(BSCollapseEvent);
                EventQue.RemoveAt(i);
            }
            return base.OnAfterRenderAsync();
        }

    }
}