using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;

namespace BlazorStrap.V4
{
    public partial class BSToast : BSToastBase
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("toast")
                .AddClass("align-items-center", Header == null)
                .AddClass($"bg-{Color.NameToLower()}", Color != BSColor.Default && IsBackgroundInRoot)
                .AddClass("position-absolute top-0 start-0", Toast == Toast.TopLeft)
                .AddClass("position-absolute top-0 start-50 translate-middle-x", Toast == Toast.TopCenter)
                .AddClass("position-absolute top-0 end-0", Toast == Toast.TopRight)
                .AddClass("position-absolute top-50 start-0 translate-middle-y", Toast == Toast.MiddleLeft)
                .AddClass("position-absolute top-50 start-50 translate-middle", Toast == Toast.MiddleCenter)
                .AddClass("position-absolute top-50 end-0 translate-middle-y", Toast == Toast.MiddleRight)
                .AddClass("position-absolute bottom-0 start-0", Toast == Toast.BottomLeft)
                .AddClass("position-absolute bottom-0 start-50 translate-middle-x", Toast == Toast.BottomCenter)
                .AddClass("position-absolute bottom-0 end-0", Toast == Toast.BottomRight)
                .AddClass("show", Shown)
                .AddClass("fade")
                .AddClass($"bg-{Color.NameToLower()}", Color != BSColor.Default && (IsBackgroundInRoot || Header == null))
                .AddClass("text-white", Color != BSColor.Warning && Color != BSColor.Default && (IsBackgroundInRoot || Header == null))
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();

        protected override string? ButtonClassBuilder => new CssBuilder()
                .AddClass("btn-close-white", Color != BSColor.Warning && Color != BSColor.Default)
                .AddClass(ButtonClass).Build().ToNullString();

        protected override string? ContentClassBuilder => new CssBuilder("toast-body")
                .AddClass(ContentClass)
                .Build().ToNullString();

        protected override string? HeaderClassBuilder => new CssBuilder("toast-header")
                .AddClass($"bg-{Color.NameToLower()}", Color != BSColor.Default && !IsBackgroundInRoot)
                .AddClass("text-white", Color != BSColor.Warning && Color != BSColor.Default && Color != BSColor.Light)
                .AddClass("text-dark", Color == BSColor.Warning && Color != BSColor.Default && Color == BSColor.Light)
                .AddClass(HeaderClass)
                .Build().ToNullString();
    }
}