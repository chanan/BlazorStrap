using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using BlazorStrap.Util;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public abstract class BSCarouselItemBase : ComponentBase, IDisposable
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Inject] Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }
        [CascadingParameter] BSCarousel Parent { get; set; }
        protected ElementReference MyRef { get; set; } 
        protected string Classname =>
        new CssBuilder("carousel-item")
        .AddClass("active", Parent.CarouselItems[Parent.ActiveIndex] == this )
        .AddClass(Class)
        .Build();
        
        protected bool AddActionLink
        {
            get
            {
                return !String.IsNullOrEmpty(ActionLink);
            }
        }

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
            if (Parent.CarouselItems[Parent.ActiveIndex] == this)
            {
                var js = new BlazorStrapInterop(JSRuntime);
                if (Parent.Direction == 0)
                {
                    var oldindex = 0;
                    if (Parent.ActiveIndex == 0) { oldindex = Parent.NumberOfItems -1; }
                    else { oldindex = Parent.ActiveIndex - 1; }

                    if (await js.AddClass(MyRef, "carousel-item-next"))
                    {
                        await js.AddClass2Elements(MyRef, Parent.CarouselItems[oldindex].MyRef, "carousel-item-left");
                    }
                }
                else if (Parent.Direction == 1)
                {
                    var oldindex = Parent.ActiveIndex + 1;
                    if (Parent.ActiveIndex == Parent.NumberOfItems - 1)
                        oldindex = 0;
                    if (await js.AddClass(MyRef, "carousel-item-prev"))
                    {
                        await js.AddClass2Elements(MyRef, Parent.CarouselItems[oldindex].MyRef, "carousel-item-right");
                    }
                }
                await new BlazorStrapInterop(JSRuntime).AddEventAnimationEnd(MyRef);
            }
            
        }

        private async Task OnAnimationEnd(string id)
        {
            var found = false;
            foreach(var item in Parent.CarouselItems)
            {
                if (id == item.MyRef.Id)
                    found = true;
            }
            if (!found)
                return;

            var js = new BlazorStrapInterop(JSRuntime);
            await js.RemoveClass(MyRef, "carousel-item-left");
            await js.RemoveClass(MyRef, "carousel-item-right");
            await js.RemoveClass(MyRef, "carousel-item-prev");
            await js.RemoveClass(MyRef, "carousel-item-next");
            if (MyRef.Id == id)
            {
                await js.AddClass(MyRef, "active");
                await js.RemoveEventAnimationEnd(MyRef);
                await Parent.Refresh();
            }
        }
       
        public void Dispose()
        {
            Parent.ActiveIndexChanged -= OnActiveIndexChanged;
            BlazorStrapInterop.OnAnimationEndEvent -= OnAnimationEnd;
        }

    }
}
