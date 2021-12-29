using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSCollapse : BlazorStrapToggleBase<BSCollapse>, IDisposable
    {
        private bool _lock;
        [Parameter] public bool NoAnimations { get; set; }
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
        public bool Shown { get; private set; }

        private string? ClassBuilder => new CssBuilder("collapse")
            .AddClass("show", Shown)
            .AddClass("navbar-collapse", IsInNavbar)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private ElementReference MyRef { get; set; }

        public override async Task ShowAsync()
        {
            if (OnShow.HasDelegate)
                await OnShow.InvokeAsync(this);
            
            if (_lock) return;
            _lock = true;
            if(!NoAnimations) await Js.InvokeVoidAsync("blazorStrap.AnimateCollapse", MyRef, true, DataId, "bsCollapse", "transitionend");
            Shown = true;
            if (NoAnimations)
                await TransitionEndAsync();
        }
        public override async Task HideAsync()
        {
            if (OnHide.HasDelegate)
                await OnHide.InvokeAsync(this);
            if (_lock) return;
            _lock = true;
            if(!NoAnimations) await Js.InvokeVoidAsync("blazorStrap.AnimateCollapse", MyRef, false, DataId, "bsCollapse", "transitionend");
            Shown = false;
            if (NoAnimations)
                await TransitionEndAsync();
        }
        
        public override Task ToggleAsync()
        {
            return Shown ? HideAsync() : ShowAsync();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if(firstRender)
            {
                _objRef = DotNetObjectReference.Create(this);
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
            _lock = false;
            await InvokeAsync(StateHasChanged);
            
            if (OnShown.HasDelegate && Shown)
                await OnShown.InvokeAsync(this);
            if (OnHidden.HasDelegate && !Shown)
                await OnHidden.InvokeAsync(this);
        }

        private async void JSCallback_ResizeEvent(int width)
        {
            if (!IsInNavbar) return;
            if (width > 576 && NavbarParent?.Expand == Size.ExtraSmall ||
                width > 576 && NavbarParent?.Expand == Size.Small ||
                width > 768 && NavbarParent?.Expand == Size.Medium ||
                width > 992 && NavbarParent?.Expand == Size.Large ||
                width > 1200 && NavbarParent?.Expand == Size.ExtraLarge ||
                width > 1400 && NavbarParent?.Expand == Size.ExtraExtraLarge)
            {
                Shown = false;
                if (OnHidden.HasDelegate && !Shown)
                    await OnHidden.InvokeAsync();
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

        public void Dispose()
        {
            JSCallback.EventHandler -= OnEventHandler;
            if(IsInNavbar)
                JSCallback.ResizeEvent -= JSCallback_ResizeEvent;
            _objRef?.Dispose();
        }
    }
}
