using BlazorComponentUtilities;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSOffCanvas  : BlazorStrapBase
    {
        [Parameter] public bool AllowScroll { get; set; }
        [Parameter] public string? BodyClass { get; set; }
        [Parameter] public string? ButtonClass { get; set; }
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;
        [Parameter] public RenderFragment? Content { get; set; }
        [Parameter] public bool DisableBackdropClick { get; set; }
        [Parameter] public RenderFragment? Header { get; set; }
        [Parameter] public string? HeaderClass { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public Placement Placement { get; set; } = Placement.Left;
        [Parameter] public bool ShowBackdrop { get; set; } = true;

        private bool _shown;

        private string? BackdropClass => new CssBuilder("offcanvas-backdrop fade")
            .AddClass("show", Shown)
            .Build().ToNullString();

        private ElementReference BackdropRef { get; set; }
        private string BackdropStyle { get; set; } = "display: none;";

        private string? BodyClassBuilder => new CssBuilder("offcanvas-body")
            .AddClass(BodyClass)
            .Build().ToNullString();

        private string? ClassBuilder => new CssBuilder("offcanvas")
            .AddClass($"offcanvas-{Placement.NameToLower().LeftRightToStartEnd()}")
            .AddClass("show", Shown)
            .AddClass($"bg-{Color.NameToLower()}", Color != BSColor.Default)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private string? HeaderClassBuilder => new CssBuilder("offcanvas-header")
            .AddClass(HeaderClass)
            .Build().ToNullString();

        private ElementReference MyRef { get; set; }

        private bool Shown
        {
            get => _shown;
            set
            {
                _ = Task.Run(() => Task.FromResult(DoAnimationsAsync(value)));
                _shown = value;
            }
        }

        public async Task ToggleAsync()
        {
            JSCallback.EventCallback("","ModalorOffcanvas", "toggled");
            if (Js != null)
            {
                if (Shown)
                {
                    if (ShowBackdrop)
                    {
                        await Js.InvokeVoidAsync("blazorStrap.RemoveClass", BackdropRef, "show", 100);
                        BackdropStyle = "display: none;";
                    }
                }
                else
                {
                    if (ShowBackdrop)
                    {
                        await Js.InvokeVoidAsync("blazorStrap.SetStyle", BackdropRef, "display", "block", 100);
                        await Js.InvokeVoidAsync("blazorStrap.AddClass", BackdropRef, "show");
                        BackdropStyle = "display: block;";
                    }
                }
            }

            Shown = !Shown;
            await InvokeAsync(StateHasChanged);
        }
        private async Task BackdropClicked()
        {
            if (DisableBackdropClick) return;
            await ToggleAsync();
        }

        private async Task ClickEvent()
        {
            if (!OnClick.HasDelegate)
                await ToggleAsync();
            await OnClick.InvokeAsync();
        }

        private async Task DoAnimationsAsync(bool value)
        {
            if (value)
            {
                if (ShowBackdrop)
                {
                    if (!AllowScroll)
                    {
                        if (Js != null)
                        {
                            var scrollWidth = await Js.InvokeAsync<int>("blazorStrap.GetScrollBarWidth");
                            await Js.InvokeVoidAsync("blazorStrap.SetBodyStyle", "overflow", "hidden");
                            await Js.InvokeVoidAsync("blazorStrap.SetBodyStyle", "paddingRight", $"{scrollWidth}px");
                        }
                    }
                }
            }
            else
            {
                {
                    if (Js != null)
                    {
                        await Js.InvokeVoidAsync("blazorStrap.SetBodyStyle", "overflow", "");
                        await Js.InvokeVoidAsync("blazorStrap.SetBodyStyle", "paddingRight", "");
                    }
                    
                }
            }
        }
    }
}