using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Service;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.InternalComponents
{
    public partial class Backdrop : ComponentBase
    {
        [Parameter] public bool IsOffcanvas { get; set; }
        protected BlazorStrapCore BlazorStrapService => (BlazorStrapCore)BlazorStrapSrc;
        // ReSharper disable once NullableWarningSuppressionIsUsed
        [Inject] public IBlazorStrap BlazorStrapSrc { get; set; } = null!;
        // ReSharper disable once NullableWarningSuppressionIsUsed
        public bool Shown { get; private set; }
        private string? BackdropClass => new CssBuilder()
            .AddClass("modal-backdrop", !IsOffcanvas)
            .AddClass("offcanvas-backdrop", IsOffcanvas)
            .AddClass("fade")
            .AddClass("show", Shown)
            .Build().ToNullString();

        private ElementReference BackdropRef { get; set; }

        private string BackdropStyle { get; set; } = "display: none;";

        public async Task ShowAsync()
        {
            await BlazorStrapService.Interop.SetStyleAsync(BackdropRef, "display", "block");
            await BlazorStrapService.Interop.AddClassAsync(BackdropRef, "show", 10);
            try
            {
                await BlazorStrapService.Interop.WaitForTransitionEnd(BackdropRef, 200);
            }
            catch { }

            BackdropStyle = "display: block;";
            Shown = true;
            await InvokeAsync(StateHasChanged);
        }
        public async Task HideAsync()
        {
            await BlazorStrapService.Interop.RemoveClassAsync(BackdropRef, "show", 10);
            try{
                await BlazorStrapService.Interop.WaitForTransitionEnd(BackdropRef, 200);
            }
            catch { }
            BackdropStyle = "display: none;";
            Shown = false;
            await InvokeAsync(StateHasChanged);

        }
        public Task ToggleAsync()
        {
            return Shown ? HideAsync() : ShowAsync();
        }
    }
}