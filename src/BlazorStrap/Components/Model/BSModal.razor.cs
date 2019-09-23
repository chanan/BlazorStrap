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
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public EventCallback<BSModalEvent> HiddenEvent { get; set; }
        [Parameter] public EventCallback<BSModalEvent> HideEvent { get; set; }
        [Parameter] public bool IgnoreClickOnBackdrop { get; set; }
        [Parameter] public bool IgnoreEscape { get; set; }
        [Parameter] public bool IsCentered { get; set; }
        [Parameter] public bool NoBackdrop { get; set; }
        [Parameter] public EventCallback<BSModalEvent> ShowEvent { get; set; }
        [Parameter] public EventCallback<BSModalEvent> ShownEvent { get; set; }
        [Parameter] public Size Size { get; set; } = Size.None;
        [Parameter] public string Style { get; set; }
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        internal BSModalEvent BSModalEvent { get; set; }
        internal List<EventCallback<BSModalEvent>> EventQue { get; set; } = new List<EventCallback<BSModalEvent>>();

        protected string Classname =>
          new CssBuilder("modal")
              .AddClass(AnimationClass, !DisableAnimations)
              .AddClass("show", (IsOpen ?? false) && DisableAnimations)
              .AddClass("show", _canShow && !DisableAnimations)
              .AddClass(Class)
          .Build();

        protected string InnerClassname =>
            new CssBuilder("modal-dialog")
                .AddClass($"modal-{Size.ToDescriptionString()}", Size != Size.None)
                .AddClass("modal-dialog-centered", IsCentered)
            .Build();

        [Inject] protected Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }
        protected ElementReference Me { get; set; }
        protected string Styles
        {
            get
            {
                var display = DisableAnimations || (IsOpen ?? true)
                    ? (IsOpen ?? false) ? "display: block; padding-right: 17px;" : null
                    : (_canShow || _drawing) ? "display: block; padding-right: 17px;" : null;
                return $"{Style} {display}".Trim();
            }
        }

        private bool _canShow { get; set; }
        private bool _drawing { get; set; }
        private bool _isInitialized { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal override async Task Changed(bool e)
        {
            if (!_isInitialized)
            {
                return;
            }
            if (!DisableAnimations)
            {
                _drawing = true;
                await new BlazorStrapInterop(JSRuntime).AddEventAnimationEnd(MyRef);
            }
            else
            {
                _canShow = true;
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
                    await Task.Delay(300).ConfigureAwait(false);
                    _canShow = true;
                    await InvokeAsync(StateHasChanged).ConfigureAwait(false);
                }).Start();
                await ShowEvent.InvokeAsync(BSModalEvent).ConfigureAwait(false);
            }
            else
            {
                await new BlazorStrapInterop(JSRuntime).RemoveClass(MyRef, "show");
                new Task(async () =>
                {
                    await Task.Delay(300).ConfigureAwait(false);
                    await new BlazorStrapInterop(JSRuntime).RemoveBodyClass("modal-open");
                }).Start();

                await HideEvent.InvokeAsync(BSModalEvent).ConfigureAwait(false);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                BlazorStrapInterop.OnAnimationEndEvent -= OnAnimationEnd;
            }
        }

        protected override Task OnAfterRenderAsync(bool firstrun)
        {
            // This is models like the demo where they are open prior to the page drawing.
            if (firstrun)
            {
                _isInitialized = true;
            }
            for (var i = 0; i < EventQue.Count; i++)
            {
                EventQue[i].InvokeAsync(BSModalEvent);
                EventQue.RemoveAt(i);
            }

            return base.OnAfterRenderAsync(false);
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
            await InvokeAsync(StateHasChanged).ConfigureAwait(false);
        }

        protected override void OnInitialized()
        {
            if (AnimationClass == null)
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
                    await ShownEvent.InvokeAsync(BSModalEvent).ConfigureAwait(false);
                }
                else
                {
                    await HiddenEvent.InvokeAsync(BSModalEvent).ConfigureAwait(false);
                }
                _canShow = IsOpen ?? false;
                _drawing = false;
                await InvokeAsync(StateHasChanged).ConfigureAwait(false);
            }
        }
    }
}
