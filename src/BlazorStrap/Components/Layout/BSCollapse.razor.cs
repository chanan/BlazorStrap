using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSCollapse : ToggleableComponentBase 
    {
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        [Parameter] protected EventCallback<BSCollapseEvent> ShowEvent { get; set; }
        [Parameter] protected EventCallback<BSCollapseEvent> ShownEvent { get; set; }
        [Parameter] protected EventCallback<BSCollapseEvent> HideEvent { get; set; }
        [Parameter] protected EventCallback<BSCollapseEvent> HiddenEvent { get; set; }

        [CascadingParameter] protected BSNavbar Navbar { get; set; }
        internal BSCollapseEvent BSCollapseEvent { get; set; }
        internal List<EventCallback<BSCollapseEvent>> EventQue { get; set; } = new List<EventCallback<BSCollapseEvent>>();

        protected string classname =>
         new CssBuilder("collapse")
             .AddClass("navbar-collapse", IsNavbar)
             .AddClass("show", IsOpen.HasValue && IsOpen.Value)
             .AddClass(Class)
         .Build();

        [Parameter] protected bool IsNavbar { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    

        protected override void OnInit()
        {
            if(IsNavbar && Navbar != null)
            {
                Navbar.VisableChange += OnVisableChange;
            }
        }

        private void OnVisableChange(object sender, bool e)
        {
            _isOpen = e;
            Invoke(() => { IsOpenChanged.InvokeAsync(e); });
            
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