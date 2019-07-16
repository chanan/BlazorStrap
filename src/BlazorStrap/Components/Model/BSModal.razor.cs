using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public class CodeBSModal : ToggleableComponentBase
    {
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
        protected void onclick()
        {
            if (!IgnoreClickOnBackdrop)
            {
                if (!_dontclickWasClicked) IsOpen = false;
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
