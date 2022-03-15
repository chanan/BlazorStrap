using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public abstract class BSInputBase<TValue> : BlazorInputBase<TValue>, IBlazorStrapBase, IDisposable
    {
        [Parameter] public int DebounceInterval { get; set; } = 500;
        [Parameter] public string InvalidClass { get; set; } = "is-invalid";
        [Parameter] public bool IsDisabled { get; set; }
        [Parameter] public bool IsInvalid { get; set; }
        [Parameter] public bool IsValid { get; set; }
        [Parameter] public EventCallback<FocusEventArgs> OnBlur { get; set; }
        [Parameter] public EventCallback<FocusEventArgs> OnFocus { get; set; }
        [Parameter] public bool ValidateOnBlur { get; set; } = true;
        [Parameter] public bool ValidateOnChange { get; set; }
        [Parameter] public bool ValidateOnInput { get; set; } = false;
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
                //Field Changed
                EditContext.OnFieldChanged += OnFieldChanged;
                // Submitted
                EditContext.OnValidationRequested += OnValidationRequested;
            }
        }

        protected void OnInputEvent(string? e)
        {
            if (ValidateOnInput && EditContext != null)
                RateLimitingExceptionForObject.Debounce(e, DebounceInterval,
                    (CurrentValueAsString) => { InvokeAsync(() => OnChangeEvent(e)); });
            if(UpdateOnInput)
                RateLimitingExceptionForObject.Debounce(e, DebounceInterval,
                    (CurrentValueAsString) => { InvokeAsync(() => OnChangeEvent(e)); });
        }

        private void DoValidation()
        {
            if (EditContext is null)
            {
                return;
            }

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

        private void OnFieldChanged(object? sender, FieldChangedEventArgs e)
        {
            if (e.FieldIdentifier.Equals(FieldIdentifier))
                DoValidation();
        }

        private void OnValidationRequested(object? sender, ValidationRequestedEventArgs e)
        {
            DoValidation();
        }

        #region Dispose

        private void Dispose()
        {
            if (EditContext is not null)
            {
                EditContext.OnFieldChanged -= OnFieldChanged;
                EditContext.OnValidationRequested -= OnValidationRequested;
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