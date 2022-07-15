using BlazorComponentUtilities;
using BlazorStrap.Bootstrap.Interfaces;
using BlazorStrap.Components.Base;
using BlazorStrap.Components.Interfaces;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public abstract class BSInputBase<TValue> : BlazorInputBase<TValue>, IBlazorStrapBase, IDisposable
    {
        /// <summary>
        /// Debounce interval in ms to filter input. Default is 500ms.
        /// </summary>
        [Parameter] public int DebounceInterval { get; set; } = 500;

        /// <summary>
        /// CSS class to apply when input is invalid. Defaults to <c>is-invalid</c>
        /// </summary>
        [Parameter] public string InvalidClass { get; set; } = "is-invalid";

        /// <summary>
        /// Sets input as disabled.
        /// </summary>
        [Parameter] public bool IsDisabled { get; set; }

        /// <summary>
        /// Is the input invalid.
        /// </summary>
        [Parameter] public bool IsInvalid { get; set; }

        /// <summary>
        /// Is the input valid.
        /// </summary>
        [Parameter] public bool IsValid { get; set; }

        /// <summary>
        /// Event called when the OnBlur event occurs.
        /// </summary>
        [Parameter] public EventCallback<FocusEventArgs> OnBlur { get; set; }

        /// <summary>
        /// Event called when the OnFocus event occurs.
        /// </summary>
        [Parameter] public EventCallback<FocusEventArgs> OnFocus { get; set; }

        /// <summary>
        /// Set to validate input on blur.
        /// </summary>
        [Parameter] public bool ValidateOnBlur { get; set; } = true;

        /// <summary>
        /// Set to validate input on change.
        /// </summary>
        [Parameter] public bool ValidateOnChange { get; set; }

        /// <summary>
        /// Set to validate input on input.
        /// </summary>
        [Parameter] public bool ValidateOnInput { get; set; } = false;

        /// <summary>
        /// CSS class to apply when input is valid. Defaults to <c>is-valid</c>.
        /// </summary>
        [Parameter] public string ValidClass { get; set; } = "is-valid";

        [Parameter] public bool UpdateOnInput { get; set; } = false;

        protected void OnBlurEvent(FocusEventArgs? e)
        {
            if (ValidateOnBlur && EditContext != null)
                EditContext.NotifyFieldChanged(FieldIdentifier);
            if (OnBlur.HasDelegate)
                OnBlur.InvokeAsync(e);
        }

        protected void OnChangeEvent(string? e)
        {
            CurrentValueAsString = e;
            if (ValidateOnInput && EditContext != null)
                EditContext.NotifyFieldChanged(FieldIdentifier);
        }

        protected void OnFocusEvent(FocusEventArgs? e)
        {
            if (OnFocus.HasDelegate)
                OnFocus.InvokeAsync(e);
        }

        protected override void OnInitialized()
        {
            if (EditContext is not null)
            {
                EditContext.OnValidationStateChanged += OnValidationStateChanged;
                EditContext.OnValidationRequested += EditContext_OnValidationRequested;
            }
        }

        private void EditContext_OnValidationRequested(object? sender, ValidationRequestedEventArgs e)
        {
            if (sender != null)
                ((EditContext)sender).NotifyFieldChanged(FieldIdentifier);
        }

        protected void OnInputEvent(string? e)
        {
            if (ValidateOnInput && EditContext != null)
                RateLimitingExceptionForObject.Debounce(e, DebounceInterval,
                    (_) => { InvokeAsync(() => OnChangeEvent(e)); });
            if (UpdateOnInput)
                RateLimitingExceptionForObject.Debounce(e, DebounceInterval,
                    (_) => { InvokeAsync(() => OnChangeEvent(e)); });
        }

        private void DoValidation()
        {
            if (EditContext is null)
            {
                return;
            }

            if (EditContext.IsModified(FieldIdentifier))
            {
                if (EditContext.GetValidationMessages(FieldIdentifier).Any())
                {
                    IsInvalid = true;
                    IsValid = false;
                }
                else
                {
                    IsValid = true;
                    IsInvalid = false;
                }
            }
            else
            {
                IsInvalid = false;
                IsValid = false;
            }
        }

        private void OnValidationStateChanged(object? sender, ValidationStateChangedEventArgs e)
        {
            DoValidation();
        }

        #region Dispose

        private void Dispose()
        {
            if (EditContext is not null)
            {
                EditContext.OnValidationStateChanged -= OnValidationStateChanged;
            }
        }

        #endregion

        #region BlazorStrapBase

        /// <summary>
        /// Position Helper
        /// </summary>
        [Parameter]
        public Position Position { get; set; } = Position.Default;
        //Copy Paste from BlazorStrapBase
        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] public string Class { get; set; } = "";
        [Parameter] public string DataId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Top, Bottom, Left, Right Margins
        /// </summary>
        [Parameter]
        public Margins Margin { get; set; }

        /// <summary>
        /// Bottom Margin
        /// </summary>
        [Parameter]
        public Margins MarginBottom { get; set; }

        /// <summary>
        /// End/Right Margin
        /// </summary>
        [Parameter]
        public Margins MarginEnd { get; set; }

        /// <summary>
        /// Left and Right Margins
        /// </summary>
        [Parameter]
        public Margins MarginLeftAndRight { get; set; }

        /// <summary>
        /// Start/Left Margin
        /// </summary>
        [Parameter]
        public Margins MarginStart { get; set; }

        /// <summary>
        /// Top Margin
        /// </summary>
        [Parameter]
        public Margins MarginTop { get; set; }

        /// <summary>
        /// Top and Bottom Margins
        /// </summary>
        [Parameter]
        public Margins MarginTopAndBottom { get; set; }

        /// <summary>
        /// Top, Bottom, Left, Right Padding
        /// </summary>
        [Parameter]
        public Padding Padding { get; set; }

        /// <summary>
        /// Bottom Padding
        /// </summary>
        [Parameter]
        public Padding PaddingBottom { get; set; }

        /// <summary>
        /// End/Right Padding
        /// </summary>
        [Parameter]
        public Padding PaddingEnd { get; set; }

        /// <summary>
        /// Left and Right Padding
        /// </summary>
        [Parameter]
        public Padding PaddingLeftAndRight { get; set; }

        /// <summary>
        /// Start/Left Padding
        /// </summary>
        [Parameter]
        public Padding PaddingStart { get; set; }

        /// <summary>
        /// Top Padding
        /// </summary>
        [Parameter]
        public Padding PaddingTop { get; set; }

        /// <summary>
        /// Top and Bottom Padding
        /// </summary>
        [Parameter]
        public Padding PaddingTopAndBottom { get; set; }

        protected bool EventsSet;

        /// <summary>
        /// Horizontal Gutters
        /// </summary>

        public string? LayoutClass =>
            new CssBuilder()
                .AddClass($"p-{Padding.ToIndex()}", Padding != Padding.Default)
                .AddClass($"pt-{PaddingTop.ToIndex()}", PaddingTop != Padding.Default)
                .AddClass($"pb-{PaddingBottom.ToIndex()}", PaddingBottom != Padding.Default)
                .AddClass($"ps-{PaddingStart.ToIndex()}", PaddingStart != Padding.Default)
                .AddClass($"pe-{PaddingEnd.ToIndex()}", PaddingEnd != Padding.Default)
                .AddClass($"px-{PaddingLeftAndRight.ToIndex()}", PaddingLeftAndRight != Padding.Default)
                .AddClass($"py-{PaddingTopAndBottom.ToIndex()}", PaddingTopAndBottom != Padding.Default)
                .AddClass($"m-{Margin.ToIndex()}", Margin != Margins.Default)
                .AddClass($"mt-{MarginTop.ToIndex()}", MarginTop != Margins.Default)
                .AddClass($"mb-{MarginBottom.ToIndex()}", MarginBottom != Margins.Default)
                .AddClass($"ms-{MarginStart.ToIndex()}", MarginStart != Margins.Default)
                .AddClass($"me-{MarginEnd.ToIndex()}", MarginEnd != Margins.Default)
                .AddClass($"mx-{MarginLeftAndRight.ToIndex()}", MarginLeftAndRight != Margins.Default)
                .AddClass($"my-{MarginTopAndBottom.ToIndex()}", MarginTopAndBottom != Margins.Default)
                .AddClass($"position-{Position.NameToLower()}", Position != Position.Default)
                .Build().ToNullString();

        #endregion
    }
}