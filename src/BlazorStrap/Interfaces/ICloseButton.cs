using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap.Interfaces
{
    internal interface ICloseButton : ICommonParameters, ICloseButtonParameters
    {
        void SetParameters(bool isDisabled, bool isWhite, EventCallback<MouseEventArgs> onClick, IDictionary<string, object> attributes, RenderFragment? childContent, string dataId, string? @class, string? layoutClass);
        RenderFragment Output();
        string? ClassBuilder();
    }
}
