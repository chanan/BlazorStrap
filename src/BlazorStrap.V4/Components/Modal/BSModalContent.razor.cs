using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Modal;

namespace BlazorStrap.V4
{
    public partial class BSModalContent : BSModalContentBase
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("modal-body")
                .AddClass($"bg-{ModalColor.NameToLower()}", ModalColor != BSColor.Default)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();
    }
}