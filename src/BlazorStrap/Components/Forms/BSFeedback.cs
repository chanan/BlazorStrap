using System;
using System.Linq;
using System.Linq.Expressions;
using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap
{
    public partial class BSFeedback<TValue> : BlazorStrapBase, IDisposable
    {
        private bool _hasInitialized;
        [CascadingParameter] private EditContext? CascadedEditContext { get; set; }
        protected EditContext EditContext { get; set; } = default!;
        [Parameter] public Expression<Func<TValue>> For { get; set; }
        [Parameter] public TValue IsManual { get; set; }

        private string? ClassBuilder => new CssBuilder()
            .AddClass("valid-tooltip", IsTooltip && !IsValid)
            .AddClass("valid-feedback", !IsTooltip && IsValid)
            .AddClass("invalid-tooltip", IsTooltip && IsInvalid)
            .AddClass("invalid-feedback", !IsTooltip && IsInvalid)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        [Parameter] public bool IsValid { get; set; }
        [Parameter] public bool IsInvalid { get; set; }
        [Parameter] public bool IsTooltip { get; set; }
        [Parameter] public string? ValidMessage { get; set; }
        [Parameter] public string? InvalidMessage { get; set; }
        protected internal FieldIdentifier FieldIdentifier { get; set; }

        protected override void OnParametersSet()
        {
            if (!_hasInitialized)
            {
                if (CascadedEditContext != null)
                {

                    _hasInitialized = true;
                    EditContext = CascadedEditContext;
                    FieldIdentifier = FieldIdentifier.Create(For);
                    //Field Changed
                    EditContext.OnFieldChanged += OnFieldChanged;
                    // Submitted
                    EditContext.OnValidationRequested += OnValidationRequested;
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

        private void OnFieldChanged(object? sender, FieldChangedEventArgs e)
        {
            if (e.FieldIdentifier.Equals(FieldIdentifier))
                DoValidation();
        }
        private void OnValidationRequested(object? sender, ValidationRequestedEventArgs e)
        {
            DoValidation();
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
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (EditContext != null)
            {
                if (InvalidMessage == null)
                {
                    foreach (var message in EditContext.GetValidationMessages(FieldIdentifier))
                    {
                        var first = true;
                        if (first)
                        {
                            InvalidMessage = message;
                            first = false;
                        }
                        else
                        {
                            InvalidMessage += $"<br/>{message}";
                        }
                    }
                }
            }
            if (!IsInvalid && !IsInvalid) return;
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", ClassBuilder);
            builder.AddMultipleAttributes(2, Attributes);
            builder.AddContent(3, InvalidMessage);
            builder.CloseElement();
        }
        public void Dispose()
        {
            if (EditContext is not null)
            {
                EditContext.OnFieldChanged -= OnFieldChanged;
                EditContext.OnValidationRequested -= OnValidationRequested;
            }
        }
    }
}