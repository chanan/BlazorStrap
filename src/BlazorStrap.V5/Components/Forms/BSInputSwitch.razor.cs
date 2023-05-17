using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V5
{
    public partial class BSInputSwitch<TValue> : BSInputCheckboxBase<TValue>
    {
        [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString().Replace("-", "");
        protected override string? ToggleClassBuilder => null;

        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
                .AddClass("form-check-input", !RemoveDefaultClass)
                .AddClass(ValidClass, IsValid)
                .AddClass(InvalidClass, IsInvalid)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();

        protected override string? ContainerClassBuilder => new CssBuilder()
            .AddClass("form-check form-switch")
            .AddClass(ContainerClass)
            .Build().ToNullString();
    }
}
