using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Service;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.InternalComponents
{
    public partial class Backdrop : ComponentBase
    {
        protected BlazorStrapCore BlazorStrapService => (BlazorStrapCore)BlazorStrapSrc;
        // ReSharper disable once NullableWarningSuppressionIsUsed
        [Inject] public IBlazorStrap BlazorStrapSrc { get; set; } = null!;
        // ReSharper disable once NullableWarningSuppressionIsUsed
        public bool Shown { get; private set; }
        private string? BackdropClass => new CssBuilder("modal-backdrop")
            .AddClass("fade")
            .AddClass("show", Shown)
            .Build().ToNullString();

        private ElementReference BackdropRef { get; set; }

        private string BackdropStyle { get; set; } = "display: none;";
        public async Task ToggleAsync()
        {
            if (Shown)
            {
                await BlazorStrapService.Interop.RemoveClassAsync(BackdropRef, "show", 10);
                BackdropStyle = "display: none;";
            }
            else
            {
                await BlazorStrapService.Interop.SetStyleAsync(BackdropRef, "display", "block");
                await BlazorStrapService.Interop.AddClassAsync(BackdropRef, "show", 10);
                BackdropStyle = "display: block;";
            }

            Shown = !Shown;
            await Task.Delay(200);
            await InvokeAsync(StateHasChanged);
        }
    }
}