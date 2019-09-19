using BlazorComponentUtilities;
using BlazorStrap.Util;
using BlazorStrap.Util.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public abstract class BSModalBase : ToggleableComponentBase, IDisposable
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Parameter] public bool NoBackdrop { get; set; }
        [Parameter] public EventCallback<BSModalEvent> ShowEvent { get; set; }
        [Parameter] public EventCallback<BSModalEvent> ShownEvent { get; set; }
        [Parameter] public EventCallback<BSModalEvent> HideEvent { get; set; }
        [Parameter] public EventCallback<BSModalEvent> HiddenEvent { get; set; }

        internal BSModalEvent BSModalEvent { get; set; }
        internal List<EventCallback<BSModalEvent>> EventQue { get; set; } = new List<EventCallback<BSModalEvent>>();

        [Inject] private Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }

        protected string classname =>
          new CssBuilder("modal")
              .AddClass(AnimationClass, !DisableAnimations)
              .AddClass("show", (IsOpen?? false) && DisableAnimations)
              .AddClass("show", CanShow && !DisableAnimations)
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
                var display = "";
                if (DisableAnimations || (IsOpen ?? true))
                    display = (IsOpen ?? false) ? "display: block; padding-right: 17px;" : null;
                else
                    display = (CanShow || Drawing) ? "display: block; padding-right: 17px;" : null;
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

        private bool CanShow { get; set; }
        private bool Drawing { get; set; }
        protected override void OnInitialized()
        {
            if(AnimationClass == null)
            {
                AnimationClass = "fade";
            }
            base.OnInitialized();
            BlazorStrapInterop.OnAnimationEndEvent += OnAnimationEnd;
        }

        private async Task OnAnimationEnd(string id)
        {
            BSModalEvent = new BSModalEvent() { Target = this };
            if (id != MyRef.Id)
            {
                await new BlazorStrapInterop(JSRuntime).RemoveEventAnimationEnd(MyRef);
                if (IsOpen ?? false)
                {
                    await ShownEvent.InvokeAsync(BSModalEvent);
                }
                else
                {
                    await HiddenEvent.InvokeAsync(BSModalEvent);
                }
                CanShow = IsOpen ?? false;
                Drawing = false;
                await InvokeAsync(StateHasChanged);
            }
            
        }

        public void Dispose()
        {
            BlazorStrapInterop.OnAnimationEndEvent -= OnAnimationEnd;
        }

        internal override async Task Changed(bool e)
        {
            if (!IsInitialized)
            {
                return;
            }
            if (!DisableAnimations)
            {
                Drawing = true;
                await new BlazorStrapInterop(JSRuntime).AddEventAnimationEnd(MyRef);
            }
            else
            {
                CanShow = true;
                return;
            }
            BSModalEvent = new BSModalEvent() { Target = this };
            if (e)
            {
                await new BlazorStrapInterop(JSRuntime).AddBodyClass("modal-open");
                if (!IgnoreEscape)
                {
                    await new BlazorStrapInterop(JSRuntime).ModalEscapeKey();
                    BlazorStrapInterop.OnEscapeEvent += OnEscape;
                }
                new Task(async () =>
                {
                    await Task.Delay(300);
                    CanShow = true;
                    await InvokeAsync(StateHasChanged);
                }).Start();
                await ShowEvent.InvokeAsync(BSModalEvent);
            }
            else
            {
                await new BlazorStrapInterop(JSRuntime).RemoveClass(MyRef, "show");
                new Task(async () =>
                {
                    await Task.Delay(300);
                    await new BlazorStrapInterop(JSRuntime).RemoveBodyClass("modal-open");
                }).Start();
                
                await HideEvent.InvokeAsync(BSModalEvent);
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

        protected async Task OnEscape()
        {
            Hide();
            BlazorStrapInterop.OnEscapeEvent -= OnEscape;
            await InvokeAsync(StateHasChanged);
        }
    }
}