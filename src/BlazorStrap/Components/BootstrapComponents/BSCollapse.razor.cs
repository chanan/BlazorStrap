using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSCollapse : BlazorStrapBase, IAsyncDisposable
    {
        private bool _lock;
        [Parameter] public RenderFragment? Content { get; set; }

        [Parameter] public bool DefaultShown
        {
            get => _defaultShown;
            set { _defaultShown = value; if (!_hasRendered) Shown = value; }
        }

        [Parameter] public bool IsInNavbar { get; set; }
        [Parameter] public bool IsList { get; set; }
        [Parameter] public RenderFragment? Toggler { get; set; }

        private bool _defaultShown;

        //Prevents the default state from overriding current state
        private bool _hasRendered;
        private DotNetObjectReference<BSCollapse>? _objRef;

        [CascadingParameter] public BSNavbar? NavbarParent { get; set; }

        // Can be access by @ref
        public bool Shown { get; set; }

        private string? ClassBuilder => new CssBuilder("collapse")
            .AddClass("show", Shown)
            .AddClass("navbar-collapse", IsInNavbar)
            .AddClass(LayoutClass, !String.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !String.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private ElementReference MyRef { get; set; }

        public async Task ToggleAsync()
        {
            if (_lock) return;
            _lock = true;
            if (!EventsSet)
            {
                await Js.InvokeVoidAsync("blazorStrap.AddEvent", DataId, "bsCollapse", "transitionend");
                EventsSet = true;
            }

            _objRef = DotNetObjectReference.Create(this);
            if (Shown)
            {
                var height = await Js.InvokeAsync<int>("blazorStrap.GetHeight", MyRef); 
                await Js.InvokeVoidAsync("blazorStrap.RemoveClass", MyRef, "collapse");
                await Js.InvokeVoidAsync("blazorStrap.SetStyle", MyRef,"height", $"{height}px");
                await Js.InvokeVoidAsync("blazorStrap.RemoveClass", MyRef, "show");
                await Js.InvokeVoidAsync("blazorStrap.AddClass", MyRef, "collapsing",100);
                await Js.InvokeVoidAsync("blazorStrap.SetStyle", MyRef,"height", "");
            }
            else
            {
                var height = await Js.InvokeAsync<int>("blazorStrap.PeakHeight", MyRef); 
                await Js.InvokeVoidAsync("blazorStrap.RemoveClass", MyRef, "collapse");
                await Js.InvokeVoidAsync("blazorStrap.AddClass", MyRef, "collapsing",100);
                await Js.InvokeVoidAsync("blazorStrap.SetStyle", MyRef,"height", $"{height}px");
            }
            if (await Js.InvokeAsync<bool>("blazorStrap.TransitionDidNotStart", MyRef))
            {
                await TransitionEndAsync();
            }
            
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if(firstRender)
            {
                _hasRendered = true;
            }
        }

        protected override void OnInitialized()
        {
            JSCallback.EventHandler += OnEventHandler;
            if (!IsInNavbar) return;
            JSCallback.ResizeEvent += JSCallback_ResizeEvent;
        }

        private async Task TransitionEndAsync()
        {
            
            await Js.InvokeVoidAsync("blazorStrap.SetStyle", MyRef,"height", "");
            await Js.InvokeVoidAsync("blazorStrap.RemoveClass", MyRef, "collapsing");
            await Js.InvokeVoidAsync("blazorStrap.AddClass", MyRef, "collapse");
            if (!Shown)
            {
                await Js.InvokeVoidAsync("blazorStrap.AddClass", MyRef, "show");
            }
            Shown = !Shown;
            _lock = false;
            await InvokeAsync(StateHasChanged);
        }

        private void JSCallback_ResizeEvent(int width)
        {
            if (width > 576 && NavbarParent?.Expand == Size.ExtraSmall ||
                width > 576 && NavbarParent?.Expand == Size.Small ||
                width > 768 && NavbarParent?.Expand == Size.Medium ||
                width > 992 && NavbarParent?.Expand == Size.Large ||
                width > 1200 && NavbarParent?.Expand == Size.ExtraLarge ||
                width > 1400 && NavbarParent?.Expand == Size.ExtraExtraLarge)
            {
                Shown = false;
                StateHasChanged();
            }
        }

        private async void OnEventHandler(string id, string name, string type, Dictionary<string, string>? classList, JavascriptEvent? e)
        {
            if (id.Contains(",") && name == "clickforward" && type == "click")
            {
                var ids = id.Split(",");
                if (ids.Any(q => q == DataId))
                {
                    await ToggleAsync();
                }
            }
            else if (DataId == id && name == "clickforward" && type == "click")
            {
                await ToggleAsync();
            }
            else if (DataId == id && name == "bsCollapse" && type == "transitionend")
            {
                await TransitionEndAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if(EventsSet)
                await Js.InvokeVoidAsync("blazorStrap.RemoveEvent", DataId, "bsCollapse" ,"transitionend");
            JSCallback.EventHandler -= OnEventHandler;
            if(IsInNavbar)
                JSCallback.ResizeEvent -= JSCallback_ResizeEvent;
            GC.SuppressFinalize(this);
        }
    }
}
