using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSPopover : BlazorToggleStrapBase<BSPopover>, IAsyncDisposable
    {
        private bool _isDisposing;
        [Parameter] public RenderFragment? Content { get; set; }
        /// <summary>
        /// This Parameter is intended for internal use 
        /// </summary>
        [Parameter] public string? DropdownOffset { get; set; }
        [Parameter] public bool IsNavItemList { get; set; }
        [Parameter] public RenderFragment? Header { get; set; }
        [Parameter] public BSColor HeaderColor { get; set; }

        /// <summary>
        /// This Parameter is intended for internal use 
        /// </summary>
        [Parameter] public bool IsDropdown { get; set; }

        [Parameter] public bool MouseOver { get; set; }
        [Parameter] public Placement Placement { get; set; } = Placement.Top;
        [Parameter] public string? Target { get; set; }
        private DotNetObjectReference<BSPopover>? _objRef;

        private string? ClassBuilder => new CssBuilder()
            .AddClass("popover", !IsDropdown)
            .AddClass("fade", !IsDropdown)
            .AddClass("dropdown-menu-end",  Placement == Placement.BottomEnd && IsDropdown)
            .AddClass($"bs-popover-{Placement.NameToLower().PurgeStartEnd().LeftRightToStartEnd()}", !IsDropdown)
            .AddClass("dropdown-menu", IsDropdown)
            .AddClass($"show", Shown)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private bool HasRender { get; set; }

        private string? HeaderClass => new CssBuilder("popover-header")
            .AddClass($"bg-{HeaderColor.NameToLower()}", HeaderColor != BSColor.Default)
            .Build().ToNullString();

        private ElementReference MyRef { get; set; }
        private bool Shown { get; set; }
        private string Style { get; set; } = "display:none;";

        public override async Task HideAsync()
        {
            if (OnHide.HasDelegate)
                await OnHide.InvokeAsync(this);
            Shown = false;
            await Js.InvokeVoidAsync("blazorStrap.RemoveClass", MyRef, "show", 100);
            await Js.InvokeVoidAsync("blazorStrap.SetStyle", MyRef, "display", "none");
            await Js.InvokeVoidAsync("blazorStrap.RemovePopover", MyRef, DataId);
            Style = "display:none;";
            await InvokeAsync(StateHasChanged);
            if (OnHidden.HasDelegate)
                await OnHidden.InvokeAsync(this);
        }


        public override async Task ShowAsync()
        {
            if (OnShow.HasDelegate)
                await OnShow.InvokeAsync(this);
            Shown = true;
            await Js.InvokeVoidAsync("blazorStrap.SetStyle", MyRef, "display", "");
            if (_objRef != null && !MyRef.Equals(null))
            {
                await Js.InvokeVoidAsync("blazorStrap.SetStyle", MyRef, "visibility", "hidden");
                await Js.InvokeVoidAsync("blazorStrap.AddClass", MyRef, "show");
                if(!string.IsNullOrEmpty(DropdownOffset))
                    await Js.InvokeVoidAsync("blazorStrap.AddPopover", MyRef, _objRef, Placement.Name().ToDashSeperated(), Target, DropdownOffset);
                else
                    await Js.InvokeVoidAsync("blazorStrap.AddPopover", MyRef, _objRef, Placement.Name().ToDashSeperated(), Target);
          
                if(!IsDropdown)
                    await Js.InvokeVoidAsync("blazorStrap.UpdatePopoverArrow", MyRef, _objRef, Placement.NameToLower().PurgeStartEnd(),
                        false);
                await Js.InvokeVoidAsync("blazorStrap.SetStyle", MyRef, "visibility", "");
                Style = await Js.InvokeAsync<string>("blazorStrap.GetStyle", MyRef); 
                EventsSet = true;
            }
            await InvokeAsync(StateHasChanged);
            if (OnShown.HasDelegate)
                await OnShown.InvokeAsync(this);
        }


        public override Task ToggleAsync()
        {
            return !Shown ? ShowAsync() : HideAsync();
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                HasRender = true;
                _objRef = DotNetObjectReference.Create(this);
                if (Target != null)
                {
                    if(!IsDropdown)
                        await Js.InvokeVoidAsync("blazorStrap.AddEvent", Target, "bsPopover", "click");
                    if (MouseOver)
                    {
                        await Js.InvokeVoidAsync("blazorStrap.AddEvent", Target, "bsPopover", "mouseenter");
                        await Js.InvokeVoidAsync("blazorStrap.AddEvent", Target, "bsPopover", "mouseleave");
                    }                        
                    EventsSet = true;
                }
            }
        }

        protected override void OnInitialized()
        {
            JSCallback.EventHandler += OnEventHandler;
        }

        private async void OnEventHandler(string id, string name, string type, Dictionary<string, string>? classList, JavascriptEvent? e)
        {
            if (id == Target && name == "bsPopover" && type == "click")
            {
                await ToggleAsync();
            }
            else if (id == Target && name == "bsPopover" && type == "mouseenter")
            {
                await ShowAsync();
            }
            else if (id == Target && name == "bsPopover" && type == "mouseleave")
            {
                await HideAsync();
            }
            else if (id == DataId && name == "bsPopover" && type == "click")
            {
                await ToggleAsync();
            }
            else if (name == "ModalorOffcanvas" && type == "toggled")
            {
                await HideAsync();
            }
        }


        #region  Dispose
        public async ValueTask DisposeAsync()
        {
            _objRef?.Dispose();
            JSCallback.EventHandler -= OnEventHandler;
            if (MouseOver)
            {
                await Js.InvokeVoidAsync("blazorStrap.RemoveEvent", Target, "bsPopover", "mouseenter");
                await Js.InvokeVoidAsync("blazorStrap.RemoveEvent", Target, "bsPopover", "mouseleave");
            }        
            // Prerendering error suppression 
            if (HasRender == true)
                try
                {
                    if (Target != null)
                    {
                        await Js.InvokeVoidAsync("blazorStrap.RemovePopover", MyRef, DataId);
                        if (EventsSet)
                        {
                            if(!IsDropdown)
                                await Js.InvokeVoidAsync("blazorStrap.RemoveEvent", Target, "bsPopover", "click");
                            
                            await Js.InvokeVoidAsync("blazorStrap.RemoveEvent", DataId, "null", "null");
                        }
                    }
                }
                catch (Exception ex) when (ex.GetType().Name == "JSDisconnectedException")
                {
                }
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}