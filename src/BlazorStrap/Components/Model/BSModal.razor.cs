using BlazorComponentUtilities;
using BlazorStrap.Util;
using BlazorStrap.Util.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public abstract class BSModalBase : ToggleableComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public EventCallback<BSModalEvent> HiddenEvent { get; set; }
        [Parameter] public EventCallback<BSModalEvent> HideEvent { get; set; }
        [Parameter] public bool IgnoreClickOnBackdrop { get; set; }
        [Parameter] public bool IgnoreEscape { get; set; }
        [Parameter] public bool IsCentered { get; set; }
        [Parameter] public bool IsBottom { get; set; }
        [Parameter] public bool IsFullWidth { get; set; }
        [Parameter] public bool IsFullHeight { get; set; }
        [Parameter] public bool IsFullScreen { get; set; }
        [Parameter] public bool NoBackdrop { get; set; }
        [Parameter] public EventCallback<BSModalEvent> ShowEvent { get; set; }
        [Parameter] public EventCallback<BSModalEvent> ShownEvent { get; set; }
        [Parameter] public Size Size { get; set; } = Size.None;
        [Parameter] public string Style { get; set; }
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        internal BSModalEvent BSModalEvent { get; set; }
        internal List<EventCallback<BSModalEvent>> EventQue { get; set; } = new List<EventCallback<BSModalEvent>>();

        protected string Classname =>
          new CssBuilder("modal fade")
              .AddClass("show", _toggleShow)
              //.AddClass("show", _canShow && !DisableAnimations)
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
                //   var display = DisableAnimations || (IsOpen ?? true)
                //   ? (IsOpen ?? false) ? "display: block; padding-right: 17px;" : null
                // we don't need any -> padding-right: 17px;
                var display = (_toggleModel) ? "display: block;" : null;
                return $"{Style} {display}".Trim();
            }
        }
        protected string InnerStyles
        {
            get
            {
                var fullWidthStyle = (IsFullWidth) ? "width:100%;min-width:100%;margin-left:0;margin-right:0;" : null;
                var bottomStyle = (IsBottom) ? "display: flex;-ms-flex-align: flex-end;align-items: flex-end;height:100%;min-height:100%;margin-top:0;margin-bottom:0;" : null;
                var fullHeightStyle = (IsFullHeight) ? "display: flex;-ms-flex-align: stretch;align-items: stretch;height:100%;min-height:100%;margin-top:0;margin-bottom:0;" : null;
                var fullScreenStyle = (IsFullScreen) ? "display: flex;-ms-flex-align: stretch;align-items: stretch;width:100%;min-width:100%;height:100%;min-height:100%;margin:0;" : null;
                return $" {fullWidthStyle} {bottomStyle} {fullScreenStyle} {fullHeightStyle}";
            }
        }

        private bool _toggleShow { get; set; }
        private bool _toggleModel { get; set; }
        private bool _isInitialized { get; set; }

        internal override async Task Changed(bool e)
        {
            if (!_isInitialized)
            {
                return;
            }
           
            BSModalEvent = new BSModalEvent() { Target = this };
            if (e)
            {
                await new BlazorStrapInterop(JSRuntime).AddBodyClass("modal-open");
                if (!IgnoreEscape)
                {
                    //TODO: This sucks make it better
                    await new BlazorStrapInterop(JSRuntime).ModalEscapeKey();
                    BlazorStrapInterop.OnEscapeEvent += OnEscape;
                }
                new Task(async () =>
                {
                    _toggleModel = true;
                    await InvokeAsync(StateHasChanged).ConfigureAwait(false);
                    await Task.Delay(300).ConfigureAwait(false);
                    _toggleShow = e;
                    await InvokeAsync(StateHasChanged).ConfigureAwait(false);
                }).Start();
                await ShowEvent.InvokeAsync(BSModalEvent).ConfigureAwait(false);
            }
            else
            {
                new Task(async () =>
                {
                    _toggleShow = e;
                    await InvokeAsync(StateHasChanged).ConfigureAwait(false);
                    await Task.Delay(300).ConfigureAwait(false);
                    _toggleModel = false;
                    await InvokeAsync(StateHasChanged).ConfigureAwait(false);
                }).Start();
                await HideEvent.InvokeAsync(BSModalEvent).ConfigureAwait(false);
                await new BlazorStrapInterop(JSRuntime).RemoveBodyClass("modal-open");
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
        }
    }
}
