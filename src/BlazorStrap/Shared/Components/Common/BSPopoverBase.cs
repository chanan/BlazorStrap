using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Modal;
using BlazorStrap.Shared.Components.OffCanvas;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSPopoverBase : BlazorStrapToggleBase<BSPopoverBase>, IAsyncDisposable
    {
        private Func<Task>? _callback;
        private DotNetObjectReference<BSPopoverBase>? _objectRef;

        /// <summary>
        /// Popover content.
        /// </summary>
        [Parameter]
        public RenderFragment? Content { get; set; }

        private bool _called;

        /// <summary>
        /// This Parameter is intended for internal use 
        /// </summary>
        [Parameter]
        public string? DropdownOffset { get; set; }

        /// <summary>
        /// For use when the popover is in a nav list.
        /// </summary>
        [Parameter] public bool IsNavItemList { get; set; }

        /// <summary>
        /// Popover header content.
        /// </summary>
        [Parameter]
        public RenderFragment? Header { get; set; }

        /// <summary>
        /// Background color of header.
        /// </summary>
        [Parameter] public BSColor HeaderColor { get; set; }

        /// <summary>
        /// This Parameter is intended for internal use 
        /// </summary>
        [Parameter]
        public bool IsDropdown { get; set; }

        /// <summary>
        /// Whether or not the popover is shown on mouseover
        /// </summary>
        [Parameter] public bool MouseOver { get; set; }

        /// <summary>
        /// Popover placement.
        /// </summary>
        [Parameter]
        public Placement Placement { get; set; } = Placement.Top;

        /// <summary>
        /// Data-Blazorstrap attribute value to target.
        /// </summary>
        [Parameter] public string? Target { get; set; }

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
        protected abstract string? HeaderClass { get; }

        private bool HasRender { get; set; }

        protected ElementReference? MyRef { get; set; }

        /// <summary>
        /// Whether or not the popover is shown.
        /// </summary>
        public override bool Shown { get; protected set; }

        protected string Style { get; set; } = "display:none;";

        private async Task TryCallback(bool renderOnFail = true)
        {
            try
            {
                // Check if objectRef set if not callback will be handled after render.
                // If anything fails callback will will be handled after render.
                if (_objectRef != null)
                {
                    if (_callback != null)
                    {
                        await _callback();
                        _callback = null;
                    }
                }
                else
                {
                    throw new InvalidOperationException("No object ref");
                }
            }
            catch
            {
                if (renderOnFail)
                    await InvokeAsync(StateHasChanged);
            }
        }

        /// <inheritdoc/>
        public override Task HideAsync()
        {
            _callback = async () =>
            {
                await HideActionsAsync();
            };
            return TryCallback();
        }

        private async Task HideActionsAsync()
        {
            if (!Shown) return;
            if (OnHide.HasDelegate)
                await OnHide.InvokeAsync(this);
            _called = true;
            Shown = false;
            await BlazorStrapService.Interop.RemoveClassAsync(MyRef, "show", 100);
            await BlazorStrapService.Interop.SetStyleAsync(MyRef, "display", "none");
            await BlazorStrapService.Interop.RemovePopoverAsync(MyRef, DataId);
            Style = "display:none;";
            await InvokeAsync(StateHasChanged);
        }

        /// <inheritdoc/>
        public override Task ShowAsync()
        {
            _callback = async () =>
            {
                await ShowActionsAsync();
            };
            return TryCallback();
        }

        private async Task ShowActionsAsync()
        {
            if (Target == null)
            {
                throw new NullReferenceException("Target cannot be null");
            }

            if (Shown) return;
            _called = true;
            if (OnShow.HasDelegate)
                await OnShow.InvokeAsync(this);
            Shown = true;
            await BlazorStrapService.Interop.SetStyleAsync(MyRef, "display", "");
            if (!MyRef.Equals(null))
            {
                await BlazorStrapService.Interop.SetStyleAsync(MyRef, "visibility", "hidden");
                await BlazorStrapService.Interop.AddClassAsync(MyRef, "show");
                if (!string.IsNullOrEmpty(DropdownOffset))
                    await BlazorStrapService.Interop.AddPopoverAsync(MyRef, Placement, Target, DropdownOffset);
                else
                    await BlazorStrapService.Interop.AddPopoverAsync(MyRef, Placement, Target);

                if (!IsDropdown)
                    await BlazorStrapService.Interop.UpdatePopoverArrowAsync(MyRef, Placement, false);
                await BlazorStrapService.Interop.SetStyleAsync(MyRef, "visibility", "");
                Style = await BlazorStrapService.Interop.GetStyleAsync(MyRef);
                EventsSet = true;
            }

            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Method used to dynamically create and show popover element.
        /// </summary>
        /// <param name="target">Data-Blazorstrap attribute value to target.</param>
        /// <param name="content">Popover content.</param>
        /// <param name="placement">Popover placement. See <see cref="Placement"/></param>
        /// <param name="header">Header content</param>
        /// <returns>Completed task when popover is shown.</returns>
        /// <exception cref="NullReferenceException">When <paramref name="target"/> or <paramref name="content"/> is null</exception>
        public async Task ShowAsync(string? target, string? content, Placement placement, string? header = null)
        {

            if (target == null || content == null)
            {
                throw new NullReferenceException("Target and Content cannot be null");
            }
            Placement = placement;
            Target = target;
            Content = CreateFragment(content);
            if (header != null)
                Header = CreateFragment(header);

            //Hides the old pop up. Placed here allows sizing to work properly don't move
            if (Shown)
                await HideAsync();
            else
                await InvokeAsync(StateHasChanged);
            await ShowAsync();
        }

        /// <inheritdoc/>
        public override Task ToggleAsync()
        {
            return !Shown ? ShowAsync() : HideAsync();
        }

        /// <summary>
        /// Dynamically creates a popover and shows or closes if it's open.
        /// </summary>
        /// <param name="target">Data-Blazorstrap attribute value to target.</param>
        /// <param name="content">Popover content.</param>
        /// <param name="placement">Popover placement. See <see cref="Placement"/></param>
        /// <param name="header">Header content</param>
        /// <returns>Completed task when render is complete.</returns>
        public Task ToggleAsync(string? target, string? content, Placement placement, string? header = null)
        {
            return target == Target && Shown ? HideAsync() : ShowAsync(target, content, placement, header);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _objectRef = DotNetObjectReference.Create<BSPopoverBase>(this);
                BlazorStrapService.OnEventForward += InteropEventCallback;
                HasRender = true;
                if (Target != null)
                {
                    if (!IsDropdown)
                        await BlazorStrapService.Interop.AddEventAsync(_objectRef, Target, EventType.Click);
                    if (MouseOver)
                    {
                        await BlazorStrapService.Interop.AddEventAsync(_objectRef, Target, EventType.Mouseenter);
                        await BlazorStrapService.Interop.AddEventAsync(_objectRef, Target, EventType.Mouseleave);
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
            if (_callback != null)
            {
                await _callback.Invoke();
                _callback = null;
            }
        }

        public override async Task InteropEventCallback(string id, CallerName name, EventType type)
        {
            if (id == Target && name.Equals(typeof(ClickForward)) && type == EventType.Click)
            {
                await ToggleAsync();
            }
            else if ((name.Equals(typeof(BSModalBase)) || name.Equals(typeof(BSOffCanvasBase))) && type == EventType.Toggle)
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
            else if (id == Target && name.Equals(this) && type == EventType.Mouseleave)
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
            _objectRef?.Dispose();
            BlazorStrapService.OnEventForward -= InteropEventCallback;
            if (Target != null)
            {
                if (MouseOver)
                {
                    try
                    {
                        await BlazorStrapService.Interop.RemoveEventAsync(this, Target, EventType.Mouseenter);
                        await BlazorStrapService.Interop.RemoveEventAsync(this, Target, EventType.Mouseleave);
                    }
                    catch { }
                }
            }

            // Prerendering error suppression 
            if (HasRender)
                try
                {
                    if (Target != null)
                    {
                        try
                        {
                            await BlazorStrapService.Interop.RemovePopoverAsync(MyRef, DataId);
                            if (EventsSet)
                            {
                                if (!IsDropdown)
                                    await BlazorStrapService.Interop.RemoveEventAsync(this, Target, EventType.Click);
                            }
                        }
                        catch { }
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
