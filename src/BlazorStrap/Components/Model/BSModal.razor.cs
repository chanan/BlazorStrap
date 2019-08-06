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
    public class CodeBSModal : ToggleableComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        [Parameter] protected EventCallback<BSModalEvent> ShowEvent { get; set; }
        [Parameter] protected EventCallback<BSModalEvent> ShownEvent { get; set; }
        [Parameter] protected EventCallback<BSModalEvent> HideEvent { get; set; }
        [Parameter] protected EventCallback<BSModalEvent> HiddenEvent { get; set; }

        internal BSModalEvent BSModalEvent { get; set; }
        internal List<EventCallback<BSModalEvent>> EventQue { get; set; } = new List<EventCallback<BSModalEvent>>();

        [Inject] Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }
        protected string classname =>
          new CssBuilder("modal")
              .AddClass("fade show", _manual == null && _isOpen)
              .AddClass("fade show", _manual != null && IsOpen.HasValue && IsOpen.Value)
              .AddClass(Class)
          .Build();

        protected string innerClassname =>
            new CssBuilder("modal-dialog")
                .AddClass($"modal-{Size.ToDescriptionString()}", Size != Size.None)
                .AddClass("modal-dialog-centered", IsCentered)
            .Build();

        protected ElementRef Me { get; set; }
        private bool Closed { get; set; }
        protected override async Task OnAfterRenderAsync()
        {
            // Sets Focus inside model so escape key can work.
            if (JustOpened)
            {
                await new BlazorStrapInterop(JSRuntime).ChangeBody(_isOpen ? "modal-open" : null);
                await new BlazorStrapInterop(JSRuntime).ChangeBodyModal(_isOpen ? "17px" : null);
                if (!IgnoreEscape)
                {
                    await new BlazorStrapInterop(JSRuntime).ModalEscapeKey();
                    BlazorStrapInterop.OnEscapeEvent += OnEscape;
                }
                JustOpened = false;
            }
            else if (Closed)
            {
                Closed = false;
                await new BlazorStrapInterop(JSRuntime).ChangeBody(_isOpen ? "modal-open" : null);
                await new BlazorStrapInterop(JSRuntime).ChangeBodyModal(_isOpen ? "17px" : null);
            }
            for (int i = 0; i < EventQue.Count; i++)
            {
                await EventQue[i].InvokeAsync(BSModalEvent);
                EventQue.RemoveAt(i);
            }
        }

        protected string styles
        {
            get
            {
                var display = "";
                if (_manual != null)
                {
                    display = (IsOpen.HasValue && IsOpen.Value) ? "display: block; padding-right: 17px;" : null;
                }
                else
                {
                    display = _isOpen ? "display: block; padding-right: 17px;" : null;
                }
                return $"{Style} {display}".Trim();
            }
        }

        [Parameter] protected bool IsCentered { get; set; }
        [Parameter] protected Size Size { get; set; } = Size.None;
        [Parameter] protected string Class { get; set; }
        [Parameter] protected string Style { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [Parameter] protected bool IgnoreClickOnBackdrop { get; set; }
        [Parameter] protected bool IgnoreEscape { get; set; }

        internal override void Changed(bool e)
        {
            BSModalEvent = new BSModalEvent() { Target = this };
            if (e)
            {
                ShowEvent.InvokeAsync(BSModalEvent);
                EventQue.Add(ShownEvent);
            }
            else
            {
                Closed = true;
                HideEvent.InvokeAsync(BSModalEvent);
                EventQue.Add(HiddenEvent);
            }
        }

        protected void OnBackdropClick()
        {
            if (!IgnoreClickOnBackdrop)
            {
                Closed = true;
                if (_manual != null) IsOpen = false;
                else if(_manual == null) _isOpen = false;
                StateHasChanged();
            }
        }
        protected void OnEscape(object sender, EventArgs e)
        {
            _isOpen = false;
            Closed = true;
            IsOpenChanged.InvokeAsync(false);
            BlazorStrapInterop.OnEscapeEvent -= OnEscape;
            Invoke(StateHasChanged);
        }
       

    }
}
