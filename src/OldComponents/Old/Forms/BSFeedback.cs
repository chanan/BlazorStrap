using System.Linq.Expressions;
using System.Web;
using BlazorComponentUtilities;
using BlazorStrap.Components.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap
{
    public class BSFeedback<TValue> : LayoutBase, IDisposable
    {
        private bool _hasInitialized;
        [CascadingParameter] private EditContext? CascadedEditContext { get; set; }
        protected EditContext? EditContext { get; set; }

        /// <summary>
        /// Input feedback is for.
        /// </summary>
        [Parameter] public Expression<Func<TValue>>? For { get; set; }
        [Parameter] public TValue? IsManual { get; set; }

        private string? ClassBuilder => new CssBuilder()
            .AddClass("valid-tooltip", IsTooltip && IsValid)
            .AddClass("valid-feedback", !IsTooltip && IsValid)
            .AddClass("invalid-tooltip", IsTooltip && IsInvalid)
            .AddClass("invalid-feedback", !IsTooltip && IsInvalid)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

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

        private string? _ActualInvalidMessage;

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

            _ActualInvalidMessage = InvalidMessage;

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
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (EditContext != null)
            {
                if (_ActualInvalidMessage == null)
                {
                    var first = true;
                    foreach (var message in EditContext.GetValidationMessages(FieldIdentifier))
                    {
                        if (first)
                        {
                            _ActualInvalidMessage = message;
                            first = false;
                        }
                        else
                        {
                            _ActualInvalidMessage += $"\n{message}";
                        }
                    }
                }
            }

            var content = IsInvalid ? _ActualInvalidMessage : IsValid ? ValidMessage : null;
            if (content == null) return;
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", ClassBuilder);
            builder.AddMultipleAttributes(2, Attributes);
            builder.AddContent(3, (MarkupString) HttpUtility.HtmlEncode(content).Replace("\n", "<br/>"));
            builder.CloseElement();
        }
        public void Dispose()
        {
            if (EditContext is null) return;
            EditContext.OnValidationStateChanged -= OnValidationStateChanged;
        }
    }
}