using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace BlazorStrap.Shared.Components.Forms
{
    public abstract class BSInputBase<TValue> : BlazorStrapInputBase<TValue>
    {
        private string _dateFormat = "yyyy-MM-dd";
        protected readonly bool IsMultipleSelect;
        public BSInputBase()
        {
            IsMultipleSelect = typeof(TValue).IsArray;
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
        /// Removes default class.
        /// </summary>
        [Parameter] public bool RemoveDefaultClass { get; set; }
        [CascadingParameter] public BSInputHelperBase? Helper { get; set; }
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
        protected override string? FormatValueAsString(TValue? value)
        {
            return value switch
            {
                null => null,
                bool @bool => BindConverter.FormatValue(@bool.ToString().ToLowerInvariant(), CultureInfo.InvariantCulture),
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

        protected override bool TryParseValueFromString(string? value, out TValue result, [NotNullWhen(false)] out string? validationErrorMessage)
        {
            return TryParseString<TValue>.ToValue(value, out result, out validationErrorMessage);
        }
        // Microsoft
        protected void SetCurrentValueAsStringArray(string?[]? value)
        {
            CurrentValue = BindConverter.TryConvertTo<TValue>(value, CultureInfo.CurrentCulture, out var result)
                ? result
                : default;
        }
    }
}
