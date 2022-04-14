using System.Reflection.Metadata;
using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSPopover : BlazorStrapToggleBase<BSPopover>, IAsyncDisposable
    {
        private DotNetObjectReference<BSPopover> _objectRef;
        [Parameter]
        public RenderFragment? Content { get; set; }

        private bool _called;

        /// <summary>
        /// This Parameter is intended for internal use 
        /// </summary>
        [Parameter]
        public string? DropdownOffset { get; set; }

        [Parameter] public bool IsNavItemList { get; set; }
        [Parameter]
        public RenderFragment? Header { get; set; }
        [Parameter] public BSColor HeaderColor { get; set; }

        /// <summary>
        /// This Parameter is intended for internal use 
        /// </summary>
        [Parameter]
        public bool IsDropdown { get; set; }

        [Parameter] public bool MouseOver { get; set; }
        [Parameter]
        public Placement Placement { get; set; } = Placement.Top;
        [Parameter] public string? Target { get; set; }

        private string? ClassBuilder => new CssBuilder()
            .AddClass("popover", !IsDropdown)
            .AddClass("fade", !IsDropdown)
            .AddClass("dropdown-menu-end", Placement == Placement.BottomEnd && IsDropdown)
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
        public bool Shown { get; private set; }
        private string Style { get; set; } = "display:none;";

        public override async Task HideAsync()
        {
            if (!Shown) return;

            if (OnHide.HasDelegate)
                await OnHide.InvokeAsync(this);
            _called = true;
            Shown = false;
            await BlazorStrap.Interop.RemoveClassAsync( MyRef, "show", 100);
            await BlazorStrap.Interop.SetStyleAsync(MyRef, "display", "none");
            await BlazorStrap.Interop.RemovePopoverAsync(MyRef, DataId);
            Style = "display:none;";
            await InvokeAsync(StateHasChanged);
        }

        public override async Task ShowAsync()
        {
            if (Shown) return;

            if (Target == null)
            {
                throw new NullReferenceException("Target cannot be null");
            }

            _called = true;
            if (OnShow.HasDelegate)
                await OnShow.InvokeAsync(this);
            Shown = true;
            await BlazorStrap.Interop.SetStyleAsync(MyRef, "display", "");
            if (!MyRef.Equals(null))
            {
                await BlazorStrap.Interop.SetStyleAsync(MyRef, "visibility", "hidden");
                await BlazorStrap.Interop.AddClassAsync(MyRef, "show");
                if (!string.IsNullOrEmpty(DropdownOffset))
                    await BlazorStrap.Interop.AddPopoverAsync(MyRef, Placement, Target, DropdownOffset);
                else
                    await BlazorStrap.Interop.AddPopoverAsync(MyRef, Placement, Target);

                if (!IsDropdown)
                    await BlazorStrap.Interop.UpdatePopoverArrowAsync(MyRef, Placement, false);
                await BlazorStrap.Interop.SetStyleAsync(MyRef, "visibility", "");
                Style = await BlazorStrap.Interop.GetStyleAsync(MyRef);
                EventsSet = true;
            }

            await InvokeAsync(StateHasChanged);
        }
        public async Task ShowAsync(string? target, string? content, Placement placement, string? header = null)
        {
            
            if (target == null || content == null)
            {
                throw new NullReferenceException("Target and Content cannot be null");
            }
            Placement = placement;
            Target = target;
            Content = CreateFragment(content);
            if(header != null)
                Header = CreateFragment(header);

            //Hides the old pop up. Placed here allows sizing to work properly don't move
            if (Shown)
                await HideAsync();
            else
                await InvokeAsync(StateHasChanged);
            await ShowAsync();
        }

        public override Task ToggleAsync()
        {
            return !Shown ? ShowAsync() : HideAsync();
        }
        public Task ToggleAsync(string? target, string? content, Placement placement, string? header = null)
        {
            return target == Target && Shown ? HideAsync() : ShowAsync(target, content, placement, header);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                HasRender = true;
                if (Target != null)
                {
                    if (!IsDropdown)
                        await BlazorStrap.Interop.AddEventAsync(_objectRef, Target, EventType.Click);
                    if (MouseOver)
                    {
                        await BlazorStrap.Interop.AddEventAsync(_objectRef, Target, EventType.Mouseenter);
                        await BlazorStrap.Interop.AddEventAsync(_objectRef, Target, EventType.Mouseleave);
                    }

                    EventsSet = true;
                }
            }
            else
            {
                if (!_called) return;
                _called = false;
                // Since there is no transition without a we run into a issue where rapid calls break the popover.
                // The delay allows the popover time to clean up
                await Task.Delay(100);
                if (Shown)
                {
                    if (OnShown.HasDelegate)
                        await OnShown.InvokeAsync(this);
                }
                else
                {
                    if (OnHidden.HasDelegate)
                        await OnHidden.InvokeAsync(this);
                }
            }
        }

        protected override void OnInitialized()
        {
            _objectRef = DotNetObjectReference.Create<BSPopover>(this);
            BlazorStrap.OnEventForward += InteropEventCallback;
        }

        public override async Task InteropEventCallback(string id, CallerName name, EventType type)
        {
            if (id == Target && name.Equals(typeof(ClickForward)) && type == EventType.Click)
            {
                await ToggleAsync();
            }
            else if ((name.Equals(typeof(BSModal)) || name.Equals(typeof(BSOffCanvas))) && type == EventType.Toggle)
            {
                await HideAsync();
            }
        }

        [JSInvokable]
        public override async Task InteropEventCallback(string id, CallerName name, EventType type, Dictionary<string, string>? classList, JavascriptEvent? e)
        {
            if (id == Target && name.Equals(this) && type == EventType.Click)
            {
                await ToggleAsync();
            }
            else if (id == Target && name.Equals(this) && type == EventType.Mouseenter)
            {
                await ShowAsync();
            }
            else if (id == Target && name.Equals(this) && type == EventType.Mouseleave )
            {
                await HideAsync();
            }
            else if (id == DataId && name.Equals(this) && type == EventType.Click)
            {
                await ToggleAsync();
            }
          
        }
        
        #region Dispose

        public async ValueTask DisposeAsync()
        {
            _objectRef.Dispose();
            BlazorStrap.OnEventForward -= InteropEventCallback;
            if (Target != null)
            {
                if (MouseOver)
                {
                    await BlazorStrap.Interop.RemoveEventAsync(this, Target, EventType.Mouseenter);
                    await BlazorStrap.Interop.RemoveEventAsync(this, Target, EventType.Mouseleave);
                }
            }

            // Prerendering error suppression 
            if (HasRender)
                try
                {
                    if (Target != null)
                    {
                        await BlazorStrap.Interop.RemovePopoverAsync(MyRef, DataId);
                        if (EventsSet)
                        {
                            if (!IsDropdown)
                                await BlazorStrap.Interop.RemoveEventAsync(this, Target, EventType.Click);
                        }
                    }
                }
                catch (Exception ex) when (ex.GetType().Name == "JSDisconnectedException")
                {
                }
        }
        private RenderFragment CreateFragment(string value) => (builder) => builder.AddMarkupContent(0, value);
        #endregion
    }
}