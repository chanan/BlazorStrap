using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Forms
{
    public interface IBSForm
    {
        event Action? OnResetEventHandler;
        void Refresh();
    }
}