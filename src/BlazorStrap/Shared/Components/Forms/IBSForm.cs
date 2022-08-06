using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public interface IBSForm 
    {
        event Action? OnResetEventHandler;
        void Refresh();
        public void Reset();
    }
}