using System.Timers;
using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSToast : BlazorStrapBase, IDisposable
    {
        
        private int CloseAfter { get; set; } = 0;
        private int TimeRemaining { get; set; } = 0;
        private bool _showTimer = false;
        private ElementReference MyRef { get; set; }
        [Parameter] public Guid? ToasterId { get; set; } = null;
        [Parameter] public bool IsBackgroundInRoot { get; set; }
        [Parameter] public string? ButtonClass { get; set; }
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;
        [Parameter] public RenderFragment? Content { get; set; }
        [Parameter] public Toast Toast { get; set; } = Toast.Default;
        [Parameter] public string? ContentClass { get; set; }
        [Parameter] public RenderFragment? Header { get; set; }
        [Parameter] public string? HeaderClass { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        private string? ClassBuilder => new CssBuilder("toast")
            .AddClass("align-items-center",Header == null )
            .AddClass($"bg-{Color.NameToLower()}", Color != BSColor.Default && IsBackgroundInRoot)
            .AddClass("position-absolute top-0 start-0", Toast == Toast.TopLeft)
            .AddClass("position-absolute top-0 start-50 translate-middle-x", Toast == Toast.TopCenter)
            .AddClass("position-absolute top-0 end-0", Toast == Toast.TopRight)
            .AddClass("position-absolute top-50 start-0 translate-middle-y", Toast == Toast.MiddleLeft)
            .AddClass("position-absolute top-50 start-50 translate-middle", Toast == Toast.MiddleCenter)
            .AddClass("position-absolute top-50 end-0 translate-middle-y", Toast == Toast.MiddleRight)
            .AddClass("position-absolute bottom-0 start-0", Toast == Toast.BottomLeft)
            .AddClass("position-absolute bottom-0 start-50 translate-middle-x", Toast == Toast.BottomCenter)
            .AddClass("position-absolute bottom-0 end-0", Toast == Toast.BottomRight)
            .AddClass("show", Shown)
            .AddClass("fade")
            .AddClass($"bg-{Color.NameToLower()}", Color != BSColor.Default && (IsBackgroundInRoot || Header == null))
            .AddClass("text-white", Color != BSColor.Warning && Color != BSColor.Default && (IsBackgroundInRoot || Header == null))
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private string? ButtonClassBuilder => new CssBuilder()
            .AddClass("btn-close-white", Color != BSColor.Warning && Color != BSColor.Default)
            .AddClass(ButtonClass).Build().ToNullString();
        
        private string? ContentClassBuilder => new CssBuilder("toast-body")
            .AddClass(ContentClass)
           .Build().ToNullString();

        private string? HeaderClassBuilder => new CssBuilder("toast-header")
            .AddClass($"bg-{Color.NameToLower()}", Color != BSColor.Default && !IsBackgroundInRoot)
            .AddClass("text-white", Color != BSColor.Warning && Color != BSColor.Default && Color != BSColor.Light)
            .AddClass("text-dark", Color == BSColor.Warning && Color != BSColor.Default && Color == BSColor.Light)
           .AddClass(HeaderClass)
           .Build().ToNullString();

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
                var self = BlazorStrap.Toaster.Children.FirstOrDefault(q => q.Id == ToasterId);
                if(self != null)
                {
                    
                    if (self.Timer == null) return;
                    self.Timer.Elapsed += TimerOnElapsed;
                    CloseAfter = self.CloseAfter;
                    if (self?.Timer?.Enabled == false && CloseAfter != 0)
                    {
                        await BlazorStrap.Interop.ToastTimerAsync(MyRef, CloseAfter, 0, self.Rendered);
                        self.Timer.Start();
                    }
                    else
                    {
                        await BlazorStrap.Interop.ToastTimerAsync(MyRef, CloseAfter, Convert.ToInt32(self?.Timer?.TimeLeft ?? 0), self?.Rendered ?? true);
                    }
                    self.Rendered = true;
                }
            }
        }

        private async void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            // Delay for animation .15 seconds = 150 ms move this to transition end later
            await BlazorStrap.Interop.AddClassAsync(MyRef, "showing");
            await Task.Delay(150);
            if(BlazorStrap.Toaster.Children.FirstOrDefault(q => q.Id == ToasterId) != null )
                BlazorStrap.Toaster.RemoveChild(ToasterId);
            
        }

        private async Task ClickEvent()
        {
            // Delay for animation .15 seconds = 150 ms move this to transition end later
            await BlazorStrap.Interop.AddClassAsync(MyRef, "showing");
            await Task.Delay(150);
            if(BlazorStrap.Toaster.Children.FirstOrDefault(q => q.Id == ToasterId) != null )
                BlazorStrap.Toaster.RemoveChild(ToasterId);
            
            if (!OnClick.HasDelegate)
                Toggle();
            await OnClick.InvokeAsync();
        }

        public void Dispose()
        {
            var self = BlazorStrap.Toaster.Children.FirstOrDefault(q => q.Id == ToasterId);
            if (self != null && self.Timer != null)
            {
                self.Timer.Elapsed += TimerOnElapsed;
            }
        }
    }
}