using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Interfaces
{
    internal interface IAlert : ICommonParameters, IAlertParameters
    {
        void SetParameters(BSColor color, RenderFragment? content, EventCallback dismissed, bool hasIcon, RenderFragment? header, int heading, bool isDismissible, IDictionary<string, object> attributes, RenderFragment? childContent, string dataId, string? @class, string? layoutClass);
        RenderFragment Output();
        string? ClassBuilder();
    }
}
