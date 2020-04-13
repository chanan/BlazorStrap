using BlazorComponentUtilities;
using BlazorStrap.Util;
using BlazorStrap.Util.Components;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        protected bool Collapsing { get; set; }
        protected string Classname =>
         new CssBuilder(Collapsing && (IsOpen ?? false) ? "collapsing" : "collapse")
             .AddClass("navbar-collapse", IsNavbar)
             .AddClass("show", (IsOpen ?? false) && !Collapsing)
             .AddClass(Class)
         .Build();

        [Parameter] public bool IsNavbar { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Inject] protected Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }
        protected override void OnInitialized()
        {
            if (IsNavbar && Navbar != null)
            {
                Navbar.VisibleChange += OnVisibleChange;
            }
            if (CollapseItem != null)
            {
                CollapseItem.Collapse = this;
            }
        }

        private void OnVisibleChange(object sender, bool e)
        {
            IsOpen = e;
            InvokeAsync(() => { IsOpenChanged.InvokeAsync(e); });
        }

        internal override async Task Changed(bool e)
        {
            Collapsing = true;
            
            BSCollapseEvent = new BSCollapseEvent() { Target = this };
            if (e)
            {
                await ShowEvent.InvokeAsync(BSCollapseEvent).ConfigureAwait(false);
                EventQue.Add(ShownEvent);
            }
            else
            {
                if (IsNavbar && Navbar != null)
                {
                    if (Navbar.HasCollapsed == false)
                    {
                        Navbar.HasCollapsed = true;
                        Navbar.Visible = false;
                    }
                }
                await HideEvent.InvokeAsync(BSCollapseEvent).ConfigureAwait(false);
                EventQue.Add(HiddenEvent);
            }
        }
        public async Task AnimationEnd()
        {
            Collapsing = false;
            await new BlazorStrapInterop(JSRuntime).ClearOffsetHeight(MyRef);
            await InvokeAsync(StateHasChanged).ConfigureAwait(false);
        }

        protected override async Task OnAfterRenderAsync(bool firstrun)
        {
            if (Collapsing)
            {
                if (firstrun && Collapsing)
                {
                    Collapsing = false;
                    await InvokeAsync(StateHasChanged).ConfigureAwait(false);
                }
                else
                {
                    await new BlazorStrapInterop(JSRuntime).SetOffsetHeight(MyRef, IsOpen ?? false);
                }
            }
            for (var i = 0; i < EventQue.Count; i++)
            {
                EventQue[i].InvokeAsync(BSCollapseEvent);
                EventQue.RemoveAt(i);
            }
           // return base.OnAfterRenderAsync(false);
        }
    }
}
