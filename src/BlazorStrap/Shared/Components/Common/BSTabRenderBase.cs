using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Common
{
    public class BSTabRenderBase : ComponentBase, IDisposable
    {
        [CascadingParameter] public BSTabWrapperBase? TabWrapper { get; set; }
        protected override void OnInitialized()
        {
            if(TabWrapper != null && TabWrapper.Nav != null)
                TabWrapper.Nav.ChildHandler += NavOnChildHandler;
        }

        private async void NavOnChildHandler(BSNavItemBase obj)
        {
            await RefreshAsync();
        }

        public void Refresh()
        {
            StateHasChanged();
        }

        public async Task RefreshAsync()
        {
            await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            if(TabWrapper != null && TabWrapper.Nav != null)
                TabWrapper.Nav.ChildHandler -= NavOnChildHandler;
        }
    }
}