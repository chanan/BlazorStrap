using System.Threading.Tasks;
using BlazorComponentUtilities;
using BlazorStrap.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap.InternalComponents;

public partial class Backdrop : ComponentBase
{
    // ReSharper disable once NullableWarningSuppressionIsUsed
    [Inject] private IBlazorStrap BlazorStrapService { get; set; } = default!;
    // ReSharper disable once NullableWarningSuppressionIsUsed
    [Parameter] public bool ShowBackdrop { get; set; }
    public bool Shown { get; private set; }
    private BlazorStrapCore BlazorStrap => (BlazorStrapCore)BlazorStrapService;
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
            await BlazorStrap.Interop.RemoveClassAsync(BackdropRef, "show", 10);
            BackdropStyle = "display: none;";
        }
        else
        {
            await BlazorStrap.Interop.SetStyleAsync(BackdropRef, "display", "block");
            await BlazorStrap.Interop.AddClassAsync(BackdropRef, "show", 10);
            BackdropStyle = "display: block;";
        }

        Shown = !Shown;
        await Task.Delay(200);
        await InvokeAsync(StateHasChanged);
    }
}