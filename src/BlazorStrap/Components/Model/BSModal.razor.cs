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
        [Parameter] protected EventCallback<BSModalEvent> ShowEvent { get; set; }
        [Parameter] protected EventCallback<BSModalEvent> ShownEvent { get; set; }
        [Parameter] protected EventCallback<BSModalEvent> HideEvent { get; set; }
        [Parameter] protected EventCallback<BSModalEvent> HiddenEvent { get; set; }

        internal BSModalEvent BSModalEvent { get; set; }
        internal List<EventCallback<BSModalEvent>> EventQue { get; set; } = new List<EventCallback<BSModalEvent>>();

        [Inject] Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }
        protected string classname =>
          new CssBuilder("modal")
              .AddClass("fade show", _isOpen)
              .AddClass(Class)
          .Build();

        protected string innerClassname =>
            new CssBuilder("modal-dialog")
                .AddClass($"modal-{Size.ToDescriptionString()}", Size != Size.None)
                .AddClass("modal-dialog-centered", IsCentered)
            .Build();

        protected ElementRef Me { get; set; }
        protected override async Task OnAfterRenderAsync()
        {
            await new BlazorStrapInterop(JSRuntime).ChangeBody(_isOpen ? "modal-open" : null);
            // Sets Focus inside model so escape key can work.
            if (JustOpened)
            {
                await new BlazorStrapInterop(JSRuntime).FocusElement(Me);
                JustOpened = false;
            }
            for (int i = 0; i < EventQue.Count; i++)
            {
                EventQue[i].InvokeAsync(BSModalEvent);
                EventQue.RemoveAt(i);
            }
        }

        protected string styles
        {
            get
            {
                var display = _isOpen ? "display: block; padding-right: 17px;" : null;
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

        private bool _dontclickWasClicked;

        protected override void OnInit()
        {
            OnOpenChangedEvent += OnOpenChanged;
        }

        private void OnOpenChanged(object sender, bool e)
        {
            BSModalEvent = new BSModalEvent() { Target = this };
            if (e)
            {
                ShowEvent.InvokeAsync(BSModalEvent);
                EventQue.Add(ShownEvent);
            }
            else
            {
                HideEvent.InvokeAsync(BSModalEvent);
                EventQue.Add(HiddenEvent);
            }
        }

        protected void onclick()
        {
            if (!IgnoreClickOnBackdrop)
            {
                if (!_dontclickWasClicked && _manual) IsOpen = false;
                else if(!_dontclickWasClicked && !_manual) _isOpen = false;
                _dontclickWasClicked = false;
                StateHasChanged();
            }
        }
        protected void onEscape(UIKeyboardEventArgs e)
        {
            if (e.Key.ToLower() == "escape" && !IgnoreEscape)
            {
                _isOpen = false;
                IsOpenChanged.InvokeAsync(false);
                StateHasChanged();
            }
        }
        protected void dontclick(UIMouseEventArgs e)
        {
            _dontclickWasClicked = true;
        }

    }
}
