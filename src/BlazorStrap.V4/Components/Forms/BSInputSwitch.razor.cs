using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V4
{
    public partial class BSInputSwitch<TValue> : BSInputCheckboxBase<TValue>
    {
        [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString().Replace("-", "");
        protected override string? ToggleClassBuilder => null;

        protected override string? LayoutClass => null;

        protected override string? ClassBuilder => new CssBuilder()
                .AddClass("custom-control-input", !RemoveDefaultClass)
                .AddClass(ValidClass, IsValid)
                .AddClass(InvalidClass, IsInvalid)
                //.AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();

        protected override string? ContainerClassBuilder => new CssBuilder()
            .AddClass("custom-control custom-switch")
            .AddClass(ContainerClass)
            .Build().ToNullString();
    }
}
