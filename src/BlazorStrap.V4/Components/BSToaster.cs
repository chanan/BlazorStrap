using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap.V4
{
    public class BSToaster : BSToasterBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var lastPlacment = Toast.Default;
            var building = false;
            var i = 0;
            if (BlazorStrapService == null) throw new ArgumentNullException(nameof(BlazorStrapService));
            if (BlazorStrapService.Toaster == null) throw new ArgumentNullException(nameof(BlazorStrapService.Toaster));
            foreach (var toast in BlazorStrapService.Toaster.Children.Values.OrderBy(q => q.Placement).ThenBy(q => q.Created))
            {
                if (toast.Placement != Toast.Default)
                {
                    building = true;
                    // Close and open new wrapper when changes
                    if (lastPlacment != toast.Placement && i != 0)
                    {
                        builder.CloseElement();
                        builder.OpenElement(i, "div");
                        builder.AddAttribute(i + 1, "class", GetClass(toast.Placement));
                        builder.AddAttribute(i + 2, "style", $"z-index:{ZIndex};{GetStyle(toast.Placement)}{WrapperStyle}");
                    }

                    //Open first wrapper
                    if (i == 0)
                    {
                        builder.OpenElement(i, "div");
                        builder.AddAttribute(i + 1, "class", GetClass(toast.Placement));
                        builder.AddAttribute(i + 2, "style", $"z-index:{ZIndex};{GetStyle(toast.Placement)}{WrapperStyle}");
                    }

                    builder.OpenElement(i + 3, "div");
                    builder.SetKey(toast.Id);
                    builder.AddContent(i + 4, GetFragment(toast));
                    builder.CloseElement();
                    i = i + 5;
                    if (lastPlacment != toast.Placement)
                        lastPlacment = toast.Placement;
                }
            }

            if (building)
            {
                builder.CloseElement();
            }
        }

        protected override string GetClass(Toast pos)
        {
            var position = $"position-{Position.NameToLower()}";
            if (Position == Position.Default)
                position = "";
            var rootClassBuilder = new CssBuilder("blazorstrap-toaster")
             .AddClass(WrapperClass, !string.IsNullOrEmpty(WrapperClass))
             .Build().ToNullString();
            return rootClassBuilder + $" {position}";
        }

        protected string GetStyle(Toast pos)
        {
            return WrapperStyle + pos switch
            {
                Toast.TopLeft => "top:0;left:0;",
                Toast.TopCenter => "top:0;left:50%;transform:translateX(-50%);",
                Toast.TopRight => "top:0;right:0;",
                Toast.MiddleLeft => "top:50%;left:0;transform: translateY(-50%)!important;",
                Toast.MiddleCenter => "top:50%;left:50%;transform: translate(-50%,-50%)!important;",
                Toast.MiddleRight => "top:50%;right:0;transform: translateY(-50%)!important;",
                Toast.BottomLeft => "bottom:0;left:0;",
                Toast.BottomCenter => "bottom:0;left:50%;transform: translateX(-50%)!important;",
                _ => "bottom:0;right:0;"
            };
        }

        protected override RenderFragment GetFragment(Toasts? Toast)
        {
            if (Toast == null) throw new ArgumentNullException(nameof(Toast));

            var header = new RenderFragment(childBuilder =>
            {
                childBuilder.AddContent(0, new MarkupString(Toast.HeaderText ?? ""));
            });
            var content = new RenderFragment(childBuilder =>
            {
                childBuilder.AddContent(0, new MarkupString(Toast.ContentText ?? ""));
                if (Toast.Options?.Template != null)
                {
                    childBuilder.OpenComponent(1, Toast.Options.Template);
                    childBuilder.AddAttribute(2, "Data", Toast.Options.Data);
                    childBuilder.CloseComponent();
                }
            });

            return builder =>
            {
                if (Toast.Options == null) throw new ArgumentNullException(nameof(Toast.Options));
                if (BlazorStrapService == null) throw new ArgumentNullException(nameof(BlazorStrap));
                if (BlazorStrapService.Toaster == null) throw new ArgumentNullException(nameof(BlazorStrapService.Toaster));
                if (BlazorStrapService.Toaster.OnChange == null) throw new ArgumentNullException(nameof(BlazorStrapService.Toaster.OnChange));

                builder.OpenComponent<BSToast>(0);
                builder.AddAttribute(1, "ButtonClass", Toast.Options.ButtonClass);
                builder.AddAttribute(2, "CloseAfter", Toast.Options.CloseAfter);
                builder.AddAttribute(3, "Color", Toast.Options.Color);
                if (!string.IsNullOrEmpty(Toast.HeaderText))
                {
                    builder.AddAttribute(4, "Header", header);
                }
                builder.AddAttribute(5, "Content", content);
                builder.AddAttribute(6, "ContentClass", Toast.Options.ContentClass);
                builder.AddAttribute(7, "HeaderClass", Toast.Options.HeaderClass);
                builder.AddAttribute(8, "TimeOut",
                    EventCallback.Factory.Create<BSToast>(this, BlazorStrapService.Toaster.OnChange));
                builder.AddAttribute(9, "ToasterId", Toast.Id);
                builder.AddAttribute(10, "HasIcon", Toast.Options.HasIcon);
                builder.AddAttribute(11, "style", "z-index:1080;position:relative");
                builder.CloseComponent();
            };
        }
    }
}