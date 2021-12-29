using System.Threading.Tasks;
using BlazorComponentUtilities;


using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap.InternalComponents;

public partial class Backdrop : ComponentBase
{
    // ReSharper disable once NullableWarningSuppressionIsUsed
    [Inject] private IBlazorStrapService BlazorStrapService { get; set; } = default!;
    // ReSharper disable once NullableWarningSuppressionIsUsed
    [Inject] private IJSRuntime Js { get; set; } = default!;
    [Parameter] public bool ShowBackdrop { get; set; }
    private bool Shown { get; set; }
    private BlazorStrapService BlazorStrap => (BlazorStrapService)BlazorStrapService;
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
            await Js.InvokeVoidAsync("blazorStrap.RemoveClass", BackdropRef, "show", 10);
            BackdropStyle = "display: none;";
        }
        else
        {
            await Js.InvokeVoidAsync("blazorStrap.SetStyle", BackdropRef, "display", "block");
            await Js.InvokeVoidAsync("blazorStrap.AddClass", BackdropRef, "show", 10);
            BackdropStyle = "display: block;";
        }

        Shown = !Shown;
        await Task.Delay(200);
        await InvokeAsync(StateHasChanged);
    }
}