using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Modal;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V4
{
    public partial class BSModal : BSModalBase
    {
        /// <summary>
        /// Sets the full screen modal size. Only has effect if <see cref="IsFullScreen"/> is true.
        /// See <see href="https://getbootstrap.com/docs/5.2/components/modal/#fullscreen-modal">Bootstrap Documentation</see>
        /// </summary>
        [Parameter] public Size FullScreenSize { get; set; } = Size.None;

        /// <summary>
        /// Sets modal size.
        /// See <see href="https://getbootstrap.com/docs/5.2/components/modal/#optional-sizes">Bootstrap Documentation</see>
        /// </summary>
        [Parameter] public Size Size { get; set; } = Size.None;

        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("modal")
                .AddClass("fade")
                .AddClass("show", Shown)
                //     .AddClass($"modal-{Size.ToDescriptionString()}", Size != Size.None)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();

        protected override string? BodyClassBuilder => new CssBuilder("modal-body")
                .AddClass(BodyClass)
                .Build().ToNullString();

        protected override string? ContentClassBuilder => new CssBuilder("modal-content")
                .AddClass($"bg-{ModalColor.NameToLower()}", ModalColor != BSColor.Default)
                .AddClass(ModalContentClass, !string.IsNullOrEmpty(ModalContentClass))
                .Build().ToNullString();

        protected override string? DialogClassBuilder => new CssBuilder("modal-dialog")
                .AddClass("modal-fullscreen", IsFullScreen && FullScreenSize == Size.None)
                .AddClass($"modal-fullscreen-{FullScreenSize.ToDescriptionString()}-down", FullScreenSize != Size.None)
                .AddClass("modal-dialog-scrollable", IsScrollable)
                .AddClass("modal-dialog-centered", IsCentered)
                .AddClass((IsScrollable ? "modal-dialog-scrollable" : string.Empty))
                .AddClass($"modal-{Size.ToDescriptionString()}", Size != Size.None)
                .AddClass("modal-dialog-centered", IsCentered)
                .AddClass(DialogClass)
                .Build().ToNullString();

        protected override string? HeaderClassBuilder => new CssBuilder("modal-header")
                .AddClass(HeaderClass)
                .Build().ToNullString();

        protected override string? FooterClassBuilder => new CssBuilder("modal-footer")
                .AddClass(FooterClass)
                .Build().ToNullString();
    }
}