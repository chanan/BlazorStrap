using BlazorStrap.Extensions;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Interfaces
{
    public interface IBSModal
    {
        bool AllowScroll { get; set; }
        string? ButtonClass { get; set; }
        RenderFragment? Content { get; set; }
        string? ContentClass { get; set; }
        string? DialogClass { get; set; }
        RenderFragment<IBSModal>? Footer { get; set; }
        string? FooterClass { get; set; }
        bool HasCloseButton { get; set; }
        RenderFragment? Header { get; set; }
        string? HeaderClass { get; set; }
        bool IsCentered { get; set; }
        bool IsFullScreen { get; set; }
        bool IsScrollable { get; set; }
        bool IsStaticBackdrop { get; set; }
        BSColor ModalColor { get; set; }
        bool ShowBackdrop { get; set; }
        bool Shown { get; }

        ValueTask DisposeAsync();
        Task HideAsync();
        Task InteropEventCallback(string id, CallerName name, EventType type);
        Task InteropEventCallback(string id, CallerName name, EventType type, Dictionary<string, string>? classList, JavascriptEvent? e);
        Task ShowAsync();
        Task ToggleAsync();
    }
}
