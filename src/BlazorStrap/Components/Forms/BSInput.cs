// Part's Flagged with Microsoft are
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap
{
    public class BSInput<TValue> : BSInputBase<TValue>
    {
        private string _dateFormat = "yyyy-MM-dd";
        // Microsoft
        private readonly bool _isMultipleSelect;
        public BSInput()
        {
            // Microsoft
            _isMultipleSelect = typeof(TValue).IsArray;
        }
        protected string Tag => InputType switch
        {
            InputType.Select => "select",
            InputType.TextArea => "textarea",
            _ => "input"
        };
        [DisallowNull] public ElementReference? Element { get; protected set; }

        /// <summary>
        /// Adds the <c>form-control-plaintext</c> class.
        /// </summary>
        [Parameter] public bool IsPlainText { get; set; }

        /// <summary>
        /// Sets the component to be readonly.
        /// </summary>
        [Parameter] public bool IsReadonly { get; set; }

        /// <summary>
        /// Form input type. Defaults to <see cref="InputType.Text"/>
        /// </summary>
        /// <remarks>If set to <see cref="InputType.Select"/> multiple select can be enabled by binding an array to the component.</remarks>
        [Parameter] public InputType InputType { get; set; } = InputType.Text;

        /// <summary>
        /// Size of input
        /// </summary>
        [Parameter] public Size InputSize { get; set; }

        private string? ClassBuilder => new CssBuilder()
            .AddClass($"form-control-{InputSize.ToDescriptionString()}", InputSize != Size.None && InputType != InputType.Select)
            .AddClass($"form-select-{InputSize.ToDescriptionString()}", InputSize != Size.None && InputType == InputType.Select)
            .AddClass("form-control", InputType != InputType.Select && InputType != InputType.Range)
            .AddClass("form-range", InputType == InputType.Range)
            .AddClass(BS.Form_Control_Plaintext, IsPlainText)
            .AddClass(ValidClass, IsValid)
            .AddClass("form-select", InputType == InputType.Select)
            .AddClass(InvalidClass, IsInvalid)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();


        protected override string? FormatValueAsString(TValue? value)
        {
            return value switch
            {
                null => null,
                int @int => BindConverter.FormatValue(@int, CultureInfo.InvariantCulture),
                long @long => BindConverter.FormatValue(@long, CultureInfo.InvariantCulture),
                float @float => BindConverter.FormatValue(@float, CultureInfo.InvariantCulture),
                double @double => BindConverter.FormatValue(@double, CultureInfo.InvariantCulture),
                decimal @decimal => BindConverter.FormatValue(@decimal, CultureInfo.InvariantCulture),
                CultureInfo @cultureInfo => BindConverter.FormatValue(@cultureInfo.Name),
                DateTime @dateTimeValue => BindConverter.FormatValue(@dateTimeValue, _dateFormat, CultureInfo.InvariantCulture),
                DateTimeOffset @dateTimeOffsetValue => BindConverter.FormatValue(@dateTimeOffsetValue, _dateFormat, CultureInfo.InvariantCulture),
#if NET6_0_OR_GREATER
                DateOnly dateOnlyValue => BindConverter.FormatValue(dateOnlyValue, _dateFormat, CultureInfo.InvariantCulture),
                TimeOnly timeOnlyValue => BindConverter.FormatValue(timeOnlyValue, _dateFormat, CultureInfo.InvariantCulture),
#endif
                _ => base.FormatValueAsString(value),
            };
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            _dateFormat = InputType switch
            {
                InputType.Time => "HH:mm:ss.fff",
                InputType.DateTimeLocal => "yyyy-MM-ddTHH:mm:ss",
                InputType.Month => "yyyy-MM",
                _ => _dateFormat
            };
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, Tag);
            if (Tag == "input")
                builder.AddAttribute(1, "type", InputType.ToDescriptionString());
            builder.AddAttribute(2, "class", ClassBuilder);
            if (_isMultipleSelect) // Microsoft
            {
                builder.AddAttribute(4, "value", BindConverter.FormatValue(CurrentValue)?.ToString());
                builder.AddAttribute(5, "onchange", EventCallback.Factory.CreateBinder<string?[]?>(this, SetCurrentValueAsStringArray, default));
            }
            else
            {
                builder.AddAttribute(3, "value", BindConverter.FormatValue(CurrentValueAsString));
                builder.AddAttribute(4, "onchange", EventCallback.Factory.CreateBinder<string?>(this, OnChangeEvent, CurrentValueAsString));
                builder.AddAttribute(5, "oninput", EventCallback.Factory.CreateBinder<string?>(this, OnInputEvent, CurrentValueAsString));
            }
            builder.AddAttribute(6, "disabled", IsDisabled);
            builder.AddAttribute(7, "readonly", IsReadonly);
            builder.AddAttribute(8, "onblur", OnBlurEvent);
            builder.AddAttribute(9, "onfocus", OnFocusEvent);
            builder.AddMultipleAttributes(8, AdditionalAttributes);
            builder.AddAttribute(10, "multiple", _isMultipleSelect);
            builder.AddElementReferenceCapture(11, elReference => Element = elReference);
            if (Tag != "input")
                builder.AddContent(12, ChildContent);
            builder.CloseElement();
        }

        protected override bool TryParseValueFromString(string? value, out TValue result, [NotNullWhen(false)] out string? validationErrorMessage)
        {
            return TryParseString<TValue>.ToValue(value, out result, out validationErrorMessage);
        }
        // Microsoft
        private void SetCurrentValueAsStringArray(string?[]? value)
        {
            CurrentValue = BindConverter.TryConvertTo<TValue>(value, CultureInfo.CurrentCulture, out var result)
                ? result
                : default;
        }
    }
}