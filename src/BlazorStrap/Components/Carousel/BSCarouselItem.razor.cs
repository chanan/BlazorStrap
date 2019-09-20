using BlazorComponentUtilities;
using BlazorStrap.Util;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public abstract class BSCarouselItemBase : ComponentBase, IDisposable
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Inject] protected Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }
        [CascadingParameter] protected BSCarousel Parent { get; set; }
        protected ElementReference MyRef { get; set; }

        protected string Classname =>
        new CssBuilder("carousel-item")
        .AddClass("active", Parent.ActiveIndex < Parent.CarouselItems.Count ? (Parent.CarouselItems[Parent.ActiveIndex] == this) : false)
        .AddClass(Class)
        .Build();

        protected bool AddActionLink => !string.IsNullOrEmpty(ActionLink);

        [Parameter] public int Interval { get; set; } = 5000;
        [Parameter] public string Src { get; set; }
        [Parameter] public string Alt { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public string ActionLink { get; set; }
        [Parameter] public string ActionLinkTarget { get; set; } = "_self";
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override void OnInitialized()
        {
            Parent.CarouselItems.Add(this);
            Parent.ActiveIndexChanged += OnActiveIndexChanged;
            BlazorStrapInterop.OnAnimationEndEvent += OnAnimationEnd;
        }

        private async Task OnActiveIndexChanged()
        {
            foreach (BSCarouselItemBase item in Parent.CarouselItems)
            {
                await new BlazorStrapInterop(JSRuntime).RemoveClass(item.MyRef, "carousel-item-left carousel-item-right carousel-item-prev carousel-item-next");
            }

            if (Parent.CarouselItems[Parent.ActiveIndex] == this)
            {
                Parent.AnimationRunning = true;
                Console.WriteLine(MyRef.Id + "-Start - " + Parent.ActiveIndex.ToString(CultureInfo.InvariantCulture));
                var js = new BlazorStrapInterop(JSRuntime);
                if (Parent.Direction == 0)
                {
                    var oldindex = Parent.ActiveIndex == 0 ? Parent.NumberOfItems - 1 : Parent.ActiveIndex - 1;

                    if (await js.AddClass(MyRef, "carousel-item-next"))
                    {
                        new Task(async () =>
                        {
                            await Task.Delay(100).ConfigureAwait(false);
                            await js.AddClass2Elements(MyRef, Parent.CarouselItems[oldindex].MyRef, "carousel-item-left");
                        }).Start();
                    }
                }
                else if (Parent.Direction == 1)
                {
                    var oldindex = Parent.ActiveIndex + 1;
                    if (Parent.ActiveIndex == Parent.NumberOfItems - 1)
                        oldindex = 0;
                    if (await js.AddClass(MyRef, "carousel-item-prev"))
                    {
                        new Task(async () =>
                        {
                            await Task.Delay(100).ConfigureAwait(false);
                            await js.AddClass2Elements(MyRef, Parent.CarouselItems[oldindex].MyRef, "carousel-item-right");
                        }).Start();
                    }
                }
            }
        }

        private async Task OnAnimationEnd(string id)
        {
            if (Parent.CarouselItems[Parent.ActiveIndex] != this) return;
            var count = Parent.CarouselItems.Where(q => q.MyRef.Id == id).Count();
            if (count > 0)
            {
                foreach (BSCarouselItemBase item in Parent.CarouselItems)
                {
                    await new BlazorStrapInterop(JSRuntime).RemoveClass(item.MyRef, "carousel-item-left carousel-item-right carousel-item-prev carousel-item-next");
                }
                await new BlazorStrapInterop(JSRuntime).AddClass(MyRef, "active");
                Parent.AnimationRunning = false;
                Parent.ResetTimer();
                await Parent.Refresh().ConfigureAwait(false);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await new BlazorStrapInterop(JSRuntime).AddEventAnimationEnd(MyRef);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Parent.ActiveIndexChanged -= OnActiveIndexChanged;
                BlazorStrapInterop.OnAnimationEndEvent -= OnAnimationEnd;
            }
        }
    }
}
