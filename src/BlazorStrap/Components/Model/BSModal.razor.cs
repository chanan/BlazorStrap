using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSModalBase : ToggleableComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Parameter] public EventCallback<BSModalEvent> ShowEvent { get; set; }
        [Parameter] public EventCallback<BSModalEvent> ShownEvent { get; set; }
        [Parameter] public EventCallback<BSModalEvent> HideEvent { get; set; }
        [Parameter] public EventCallback<BSModalEvent> HiddenEvent { get; set; }

        internal BSModalEvent BSModalEvent { get; set; }
        internal List<EventCallback<BSModalEvent>> EventQue { get; set; } = new List<EventCallback<BSModalEvent>>();

        [Inject] Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }
        protected string classname =>
          new CssBuilder("modal")
              .AddClass("fade show", (IsOpen ?? false))
              .AddClass(Class)
          .Build();

        protected string innerClassname =>
            new CssBuilder("modal-dialog")
                .AddClass($"modal-{Size.ToDescriptionString()}", Size != Size.None)
                .AddClass("modal-dialog-centered", IsCentered)
            .Build();

        protected ElementReference Me { get; set; }
        private bool IsInitialized { get; set; }
        protected override Task OnAfterRenderAsync(bool firstrun)
        {
            // This is models like the demo where they are open prior to the page drawing.
            if (firstrun)
            {
                IsInitialized = true;
            }
            for (int i = 0; i < EventQue.Count; i++)
            {
                EventQue[i].InvokeAsync(BSModalEvent);
                EventQue.RemoveAt(i);
            }

            return base.OnAfterRenderAsync(false);
        }
        protected string styles
        {
            get
            {
                var display = (IsOpen ?? false) ? "display: block; padding-right: 17px;" : null;
                return $"{Style} {display}".Trim();
            }
        }

        [Parameter] public bool IsCentered { get; set; }
        [Parameter] public Size Size { get; set; } = Size.None;
        [Parameter] public string Class { get; set; }
        [Parameter] public string Style { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public bool IgnoreClickOnBackdrop { get; set; }
        [Parameter] public bool IgnoreEscape { get; set; }

        internal override void Changed(bool e)
        {
           ChangedAsync(e);
        }
        internal async Task ChangedAsync(bool e)
        {
            if(!IsInitialized)
            {
                return;
            }
            BSModalEvent = new BSModalEvent() { Target = this };
            if (e)
            {
                await new BlazorStrapInterop(JSRuntime).ChangeBody("modal-open");
                if (!IgnoreEscape)
                {
                    await new BlazorStrapInterop(JSRuntime).ModalEscapeKey();
                    BlazorStrapInterop.OnEscapeEvent += OnEscape;
                }
                await ShowEvent.InvokeAsync(BSModalEvent);
                EventQue.Add(ShownEvent);
            }
            else
            {
                await new BlazorStrapInterop(JSRuntime).ChangeBody(null);
                await HideEvent.InvokeAsync(BSModalEvent);
                EventQue.Add(HiddenEvent);
            }
        }

        protected void OnBackdropClick()
        {
            if (!IgnoreClickOnBackdrop)
            {
                Hide();
                StateHasChanged();
            }
        }
        protected void OnEscape(object sender, EventArgs e)
        {
            Hide();
            BlazorStrapInterop.OnEscapeEvent -= OnEscape;
            InvokeAsync(StateHasChanged);
        }
       

    }
}
