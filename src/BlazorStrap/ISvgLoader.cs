using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorStrap.Extensions
{
    public interface ISvgLoader
    {
        Task<MarkupString> LoadSvg(string url);
    }
}