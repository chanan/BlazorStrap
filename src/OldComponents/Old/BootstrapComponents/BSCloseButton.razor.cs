using BlazorComponentUtilities;
using BlazorStrap.Components.Base;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public class BSCloseButton : LayoutBase
    {
        /// <summary>
        /// Whether or not the button is disabled.
        /// </summary>
        [Parameter] public bool IsDisabled { get; set; }

        /// <summary>
        /// Adds the btn-close-white class.
        /// </summary>
        [Parameter] public bool IsWhite { get; set; }

        /// <summary>
        /// Event called when button is clicked.
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected async Task ClickEventAsync(MouseEventArgs e)
        {
            if (OnClick.HasDelegate)
            {
                await EventUtil.AsNonRenderingEventHandler<MouseEventArgs>(OnClick.InvokeAsync).Invoke(e);
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (BlazorStrap.BootstrapVersion == BootstrapVersion.Bootstrap4)
                Version4RenderBuilder(builder);
            else
                Version5RenderBuilder(builder);
        }

        #region Bootstrap render support methods
        protected override string? ClassBuilder()
        {
            return BlazorStrap.BootstrapVersion == BootstrapVersion.Bootstrap4 ? Version4ClassBuilder() : Version5ClassBuilder();
        }
        #region Bootstrap 4
        protected override string? Version4ClassBuilder()
        {
            return new CssBuilder("close")
               .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
               .AddClass(Class, !string.IsNullOrEmpty(Class))
               .Build().ToNullString();
        }
        protected override void Version4RenderBuilder(RenderTreeBuilder builder)
        {
            var s = 0;
            builder.OpenElement(s, "button");
            builder.AddAttribute(s++, "class", ClassBuilder());
            builder.AddAttribute(s++, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, ClickEventAsync));
            builder.AddAttribute(s++, "disabled", IsDisabled);
            builder.AddMultipleAttributes(s++, Attributes);
            builder.OpenElement(s, "span");
            builder.AddAttribute(s, "aria-hidden", "true");
            builder.AddContent(s, new MarkupString("&times;"));
            builder.CloseElement();
            builder.CloseElement();
        }
        #endregion

        #region Bootstrap 5

        protected override string? Version5ClassBuilder()
        {
            return new CssBuilder("btn-close")
               .AddClass("btn-close-white", IsWhite)
               .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
               .AddClass(Class, !string.IsNullOrEmpty(Class))
               .Build().ToNullString();
        }
        protected override void Version5RenderBuilder(RenderTreeBuilder builder)
        {
            var s = 0;
            builder.OpenElement(s, "button");
            builder.AddAttribute(s++, "class", ClassBuilder());
            builder.AddAttribute(s++, "onclick", EventUtil.AsNonRenderingEventHandler<MouseEventArgs>(ClickEventAsync));
            builder.AddAttribute(s++, "disabled", IsDisabled);
            builder.AddMultipleAttributes(s++, Attributes);
            builder.CloseElement();
        }
        #endregion
        #endregion
    }
}