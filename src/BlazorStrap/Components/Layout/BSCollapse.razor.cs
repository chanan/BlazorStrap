using BlazorComponentUtilities;
using BlazorStrap.Util;
using BlazorStrap.Util.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public partial class BSCollapse : ToggleableComponentBase
    {
        private DotNetObjectReference<BSCollapse> _objectReference;
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
         new CssBuilder("collapse")
             .AddClass("navbar-collapse", IsNavbar)
             .AddClass("show", (IsOpen ?? false) && DisableAnimations && !Collapsing)
             .AddClass(Class)
         .Build();

        [Parameter] public bool IsNavbar { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Inject] public BlazorStrapInterop BlazorStrapInterop { get; set; }
        protected override void OnInitialized()
        {
            _objectReference = DotNetObjectReference.Create(this);
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
            await BlazorStrapInterop.AddCollapsingEvent(MyRef, e, _objectReference).ConfigureAwait(false);
            await BlazorStrapInterop.CollapsingElement(MyRef, e).ConfigureAwait(false);
            InternalIsOpen = e;
        }

        [JSInvokable]
        public async Task AnimationEnd()
        {
            Collapsing = false;
           // InvokeAsync(StateHasChanged);
        }

        protected override async Task OnAfterRenderAsync(bool firstrun)
        {
            for (var i = 0; i < EventQue.Count; i++)
            {
                await EventQue[i].InvokeAsync(BSCollapseEvent).ConfigureAwait(false);
                EventQue.RemoveAt(i);
            }
            // return base.OnAfterRenderAsync(false);
        }
    }
}
