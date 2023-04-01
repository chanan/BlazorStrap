using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Timers;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSToastBase : BlazorStrapBase, IDisposable
    {
        private int CloseAfter { get; set; } = 0;
        private int TimeRemaining { get; set; } = 0;
        protected ElementReference? MyRef { get; set; }

        /// <summary>
        /// Toaster Id. See <see cref="BSToaster"/>
        /// </summary>
        [Parameter] public Guid? ToasterId { get; set; } = null;

        [Parameter] public bool IsBackgroundInRoot { get; set; }

        /// <summary>
        /// CSS classes to be applied to the close button.
        /// </summary>
        [Parameter] public string? ButtonClass { get; set; }

        /// <summary>
        /// Toast color.
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        /// <summary>
        /// Toast content.
        /// </summary>
        [Parameter] public RenderFragment? Content { get; set; }

        /// <summary>
        /// Toast location.
        /// </summary>
        [Parameter] public Toast Toast { get; set; } = Toast.Default;

        /// <summary>
        /// CSS classes to be applied to the content.
        /// </summary>
        [Parameter] public string? ContentClass { get; set; }

        /// <summary>
        /// Toast header content.
        /// </summary>
        [Parameter] public RenderFragment? Header { get; set; }

        /// <summary>
        /// CSS classes to be applied to the toast header.
        /// </summary>
        [Parameter] public string? HeaderClass { get; set; }

        /// <summary>
        /// Event called when toast is closed.
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        /// <summary>
        /// Toast has Icon
        /// </summary>
        [Parameter] public bool HasIcon { get; set; }

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
        protected abstract string? ButtonClassBuilder { get; }
        protected abstract string? ContentClassBuilder { get; }
        protected abstract string? HeaderClassBuilder { get; }

        protected override bool ShouldRender()
        {
            return false;
        }

        public bool Shown { get; private set; } = true;

        public void Toggle()
        {
            Shown = !Shown;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var self = BlazorStrapService.Toaster.Children.FirstOrDefault(q => q.Id == ToasterId);
                if (self != null)
                {
                    if (self.Timer == null) return;
                    self.Timer.Elapsed += TimerOnElapsed;
                    CloseAfter = self.CloseAfter;

                    if (self?.Timer?.Enabled == false && CloseAfter != 0)
                    {
                        await BlazorStrapService.Interop.ToastTimerAsync(MyRef, CloseAfter, 0, self.Rendered);
                        self.Timer.Start();
                    }
                    else
                    {
                        await BlazorStrapService.Interop.ToastTimerAsync(MyRef, CloseAfter, Convert.ToInt32(self?.Timer?.TimeLeft ?? 0), self?.Rendered ?? true);
                    }
                    if (self != null)
                        self.Rendered = true;
                }
            }
        }

        private async void TimerOnElapsed(object? sender, ElapsedEventArgs e)
        {
            // If this fails the only thing that will happen, the smooth closing animation wont be shown this is completely safe to keep in try catch.
            try
            {
                await BlazorStrapService.Interop.AddClassAsync(MyRef, "showing");
            }
            catch
            { }
            await Task.Delay(150);
            if (BlazorStrapService.Toaster.Children.FirstOrDefault(q => q.Id == ToasterId) != null)
                BlazorStrapService.Toaster.RemoveChild(ToasterId);

        }

        protected async Task ClickEvent()
        {
            // Delay for animation .15 seconds = 150 ms move this to transition end later
            await BlazorStrapService.Interop.AddClassAsync(MyRef, "showing");
            await Task.Delay(150);
            if (BlazorStrapService.Toaster.Children.FirstOrDefault(q => q.Id == ToasterId) != null)
                BlazorStrapService.Toaster.RemoveChild(ToasterId);

            if (!OnClick.HasDelegate)
                Toggle();
            await OnClick.InvokeAsync();
        }

        public void Dispose()
        {
            var self = BlazorStrapService.Toaster.Children.FirstOrDefault(q => q.Id == ToasterId);
            if (self != null && self.Timer != null)
            {
                self.Timer.Elapsed += TimerOnElapsed;
            }
        }
    }
}
