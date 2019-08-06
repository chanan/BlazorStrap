using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using System.Collections.Generic;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public class CodeBSButton : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder("btn")
          .AddClass($"btn-outline-{Color.ToDescriptionString()}", IsOutline)
          .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None && !IsOutline)
          .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
          .AddClass("btn-block", IsBlock)
          .AddClass("active", ButtonType == ButtonType.Link && IsActive)
          .AddClass("disabled", ButtonType == ButtonType.Link && IsDisabled)
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

        [Parameter] protected EventCallback<UIMouseEventArgs> OnClick { get; set; }
        [Parameter] protected Color Color { get; set; } = Color.Primary;

        private ButtonType _buttonType = ButtonType.Button;
        [Parameter]
        private ButtonType ButtonType
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
        [Parameter] protected bool IsOutline { get; set; }
        [Parameter] protected Size Size { get; set; } = Size.None;
        [Parameter] protected bool IsBlock { get; set; }
        [Parameter] protected string Value { get; set; }
        [Parameter] protected bool IsActive { get; set; }
        [Parameter] protected bool IsDisabled { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
