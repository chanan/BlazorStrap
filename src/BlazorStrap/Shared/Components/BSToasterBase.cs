using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components
{
    public abstract class BSToasterBase : ComponentBase, IDisposable
    {
        [Inject] protected IBlazorStrap? BlazorStrapService { get; set; } = default!;
        private readonly Debouncer _changeDebouncer = new();

        /// <summary>
        /// Sets the wrapper class for the toaster.
        /// </summary>
        [Parameter] public string WrapperClass { get; set; } = "";
        [Parameter] public Position Position { get; set; } = Position.Fixed;
        [Parameter] public int ZIndex { get; set; } = 1025;
        [Parameter] public string? WrapperStyle { get; set; } 
        protected override void OnInitialized()
        {
            if (BlazorStrapService == null) throw new ArgumentNullException(nameof(BlazorStrapService));
            if (BlazorStrapService.Toaster == null) throw new ArgumentNullException(nameof(BlazorStrapService.Toaster));
            BlazorStrapService.Toaster.OnChange += OnChange;
        }

        protected void OnChange()
        {
            _ = InvokeAsync(() => _changeDebouncer.Debounce(50, () => InvokeAsync(StateHasChanged)));
        }

        protected abstract RenderFragment GetFragment(Toasts? Toast);

        protected abstract string GetClass(Toast pos);

 
        public void Dispose()
        {
            _changeDebouncer.Dispose();
            if (BlazorStrapService == null) throw new ArgumentNullException(nameof(BlazorStrapService));
            if (BlazorStrapService.Toaster == null) throw new ArgumentNullException(nameof(BlazorStrapService.Toaster));
            BlazorStrapService.Toaster.OnChange -= OnChange;
        }
    }
}

