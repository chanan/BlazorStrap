using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Utilities
{
    public interface ISvgLoader
    {
        Task<MarkupString> LoadSvg(string url);
    }
}