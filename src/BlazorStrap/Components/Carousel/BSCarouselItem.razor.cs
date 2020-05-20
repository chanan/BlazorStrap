using BlazorComponentUtilities;
using BlazorStrap.Util;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public abstract class BSCarouselItemBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [CascadingParameter] protected BSCarousel Parent { get; set; }
        protected ElementReference MyRef { get; set; }

        protected string Classname =>
        new CssBuilder("carousel-item")
        .AddClass("active", Active)
        .AddClass("carousel-item-left", Left)
        .AddClass("carousel-item-right", Right)
        .AddClass("carousel-item-prev", Prev)
        .AddClass("carousel-item-next", Next)
        .AddClass(Class)
        .Build();

        public bool Active { get; set; }
        public bool Left { get; set; }
        public bool Right { get; set; }
        public bool Prev { get; set; }
        public bool Next { get; set; }
        protected bool AddActionLink => !string.IsNullOrEmpty(ActionLink);
        protected bool IsSvg { get; private set; }

        [Parameter] public int Interval { get; set; } = 5000;
        [Parameter] public string Src { get; set; }
        [Parameter] public string Alt { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public string ActionLink { get; set; }
        [Parameter] public string ActionLinkTarget { get; set; } = "_self";
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override void OnInitialized()
        {
            SetSvg();
            Parent.CarouselItems.Add(this);
        }

        public async Task StateChanged()
        {
            await InvokeAsync(StateHasChanged).ConfigureAwait(false);
        }

        public Task AnimationEnd()
        {
            return Parent.AnimationEnd(this);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                if(Parent.CarouselItems[0] == this)
                {
                    Active = true;
                    await InvokeAsync(StateHasChanged).ConfigureAwait(false);
                }
            }
        }
        public Task Clean()
        {
            Active = false;
            Left = false;
            Right = false;
            Prev = false;
            Next = false;

            return Task.CompletedTask;
        }

        private void SetSvg()
        {
            IsSvg = (Path.GetExtension(Src) == ".svg" ? true : false);
            StateHasChanged();
        }
    }
}
