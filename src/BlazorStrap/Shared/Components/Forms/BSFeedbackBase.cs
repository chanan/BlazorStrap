using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace BlazorStrap.Shared.Components.Forms
{
    public abstract class BSFeedbackBase<TValue> : BlazorStrapBase
    {
        private bool _hasInitialized;
        [CascadingParameter] private EditContext? CascadedEditContext { get; set; }
        protected EditContext? EditContext { get; set; }

        /// <summary>
        /// Input feedback is for.
        /// </summary>
        [Parameter] public Expression<Func<TValue>>? For { get; set; }
        [Parameter] public TValue? IsManual { get; set; }
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }

        /// <summary>
        /// Is the input valid.
        /// </summary>
        [Parameter] public bool IsValid { get; set; }

        /// <summary>
        /// Is the input invalid.
        /// </summary>
        [Parameter] public bool IsInvalid { get; set; }

        /// <summary>
        /// Rendered as tooltip.
        /// </summary>
        [Parameter] public bool IsTooltip { get; set; }

        /// <summary>
        /// Message to show when input is valid.
        /// </summary>
        [Parameter] public string? ValidMessage { get; set; }

        /// <summary>
        /// Message to show when input is invalid.
        /// </summary>
        [Parameter] public string? InvalidMessage { get; set; }

        protected internal FieldIdentifier FieldIdentifier { get; set; }

        protected string? ActualInvalidMessage;

        protected override void OnParametersSet()
        {
            if (!_hasInitialized)
            {
                if (CascadedEditContext != null)
                {

                    _hasInitialized = true;
                    EditContext = CascadedEditContext;
                    if (For != null) FieldIdentifier = FieldIdentifier.Create(For);
                    EditContext.OnValidationStateChanged += OnValidationStateChanged;
                    DoValidation();
                }
            }
            else if (CascadedEditContext != EditContext)
            {
                // Not the first run

                // We don't support changing EditContext because it's messy to be clearing up state and event
                // handlers for the previous one, and there's no strong use case. If a strong use case
                // emerges, we can consider changing this.
                throw new InvalidOperationException($"{GetType()} does not support changing the EditContext dynamically.");
            }
        }

        private void OnValidationStateChanged(object? sender, ValidationStateChangedEventArgs e)
        {
            DoValidation();
        }
        private void DoValidation()
        {
            if (EditContext is null)
            {
                return;
            }

            ActualInvalidMessage = InvalidMessage;

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
                IsValid = false;
                IsInvalid = false;
            }
            StateHasChanged();
        }
        
        public void Dispose()
        {
            if (EditContext is null) return;
            EditContext.OnValidationStateChanged -= OnValidationStateChanged;
        }
    }
}
