using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using System.Collections.Generic;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public class BSButtonBase  : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder("btn")
          .AddClass($"btn-outline-{Color.ToDescriptionString()}", IsOutline)
          .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None && !IsOutline)
          .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
          .AddClass("btn-block", IsBlock)
          .AddClass("active", ButtonType == ButtonType.Link && IsActive)
          .AddClass("disabled", ButtonType == ButtonType.Link && IsDisabled)
          .AddClass("valid", IsValid)
          .AddClass("invalid", IsInvalid)
          .AddClass(Class)
        .Build();

        protected string Tag { get; set; } = "button";

        protected string pressed => IsActive ? "true" : null;

        protected string disabled => IsDisabled ? "true" : null;

        protected string tab => IsDisabled ? "-1" : null;

        protected string type => ButtonType switch
        {
            ButtonType.Input => "button",
            ButtonType.Button => "button",
            ButtonType.Submit => "submit",
            ButtonType.Reset => "reset",
            _ => null
        };

        [Parameter] public EventCallback<UIMouseEventArgs> OnClick { get; set; }
        [Parameter] public Color Color { get; set; } = Color.Primary;

        private ButtonType _buttonType = ButtonType.Button;
        [Parameter]
        public ButtonType ButtonType
        {
            get => _buttonType;
            set
            {
                _buttonType = value;
                Tag = _buttonType switch
                {
                    ButtonType.Button => "button",
                    ButtonType.Link => "a",
                    ButtonType.Input => "input",
                    ButtonType.Submit => "input",
                    ButtonType.Reset => "input",
                    ButtonType.Radio => "input",
                    ButtonType.Checkbox => "input",
                    _ => "button"
                };
            }
        }
        [Parameter] public bool IsOutline { get; set; }
        [Parameter] public Size Size { get; set; } = Size.None;
        [Parameter] public bool IsBlock { get; set; }
        [Parameter] public string Value { get; set; }
        [Parameter] public bool IsActive { get; set; }
        [Parameter] public bool IsDisabled { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public bool IsValid { get; set; }
        [Parameter] public bool IsInvalid { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
