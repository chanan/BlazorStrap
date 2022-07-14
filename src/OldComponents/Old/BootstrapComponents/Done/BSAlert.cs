using BlazorComponentUtilities;
using BlazorStrap.Components.Base;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public class BSAlert : LayoutBase
    {
        /// <summary>
        /// Color class of alert
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        /// <summary>
        /// Alert body content
        /// </summary>
        [Parameter] public RenderFragment? Content { get; set; }

        /// <summary>
        /// Event triggered when alert is dismissed. Only called when <see cref="IsDismissible"/> is true
        /// </summary>
        [Parameter] public EventCallback Dismissed { get; set; }

        /// <summary>
        /// Sets whether or not an icon is shown
        /// </summary>
        [Parameter] public bool HasIcon { get; set; }

        /// <summary>
        /// Alert header content (optional)
        /// </summary>
        [Parameter] public RenderFragment? Header { get; set; }

        /// <summary>
        /// Heading size. Valid values are 1-6
        /// </summary>
        [Parameter] public int Heading { get; set; } = 1;

        /// <summary>
        /// Determines whether or not an alter is dismissible. See <see cref="Dismissed"/> for the callback
        /// </summary>
        [Parameter] public bool IsDismissible { get; set; }
        private bool _isDismissed;

        /// <summary>
        /// Dismisses the alert
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        public virtual async Task CloseEventAsync()
        {
            await EventUtil.AsNonRenderingEventHandler(Dismissed.InvokeAsync).Invoke();
            _isDismissed = true;
        }

        /// <summary>
        /// Opens the alert
        /// </summary>
        public virtual void Open()
        {
            _isDismissed = false;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (_isDismissed) return;

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
            return new CssBuilder("alert")
                .AddClass($"alert-{Color.NameToLower()}", Color != BSColor.Default)
                .AddClass("d-flex align-items-center", HasIcon)
                .AddClass("alert-dismissible", IsDismissible)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();
        }

        protected override void Version4RenderBuilder(RenderTreeBuilder builder)
        {
            Version5RenderBuilder(builder);
        }
        #endregion

        #region Bootstrap 5
        protected override string? Version5ClassBuilder()
        {
            return new CssBuilder("alert")
                .AddClass($"alert-{Color.NameToLower()}", Color != BSColor.Default)
                .AddClass("d-flex align-items-center", HasIcon)
                .AddClass("alert-dismissible", IsDismissible)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();
        }

        protected override void Version5RenderBuilder(RenderTreeBuilder builder)
        {
            var s = 0;
            builder.OpenElement(s, "div");
            builder.AddAttribute(s++, "class", ClassBuilder());
            builder.AddAttribute(s++, "role", "alert");
            builder.AddMultipleAttributes(s++, Attributes);
            if (Header != null)
            {
                builder.OpenElement(s++, $"h{Heading}");
                builder.AddAttribute(s++, "class", "alert-heading");
                builder.AddContent(s++, Header);
                builder.CloseElement();
                builder.AddContent(s++, Content);
            }
            else if (HasIcon)
            {
                builder.AddContent(s++, (MarkupString)(Icons.GetAlertIcon(Color.NameToLower()) ?? ""));
                builder.OpenElement(s++, "div");
                builder.AddContent(s++, ChildContent);
                builder.CloseElement();
            }
            else
            {
                builder.AddContent(s++, ChildContent);
            }
            if (IsDismissible)
            {
                builder.OpenComponent(s++, typeof(BSCloseButton));
                builder.AddAttribute(s++, "OnClick", EventCallback.Factory.Create<MouseEventArgs>(this, EventUtil.AsNonRenderingEventHandler(CloseEventAsync)));
                builder.CloseComponent();
            }
            builder.CloseElement();
        }
        #endregion
        #endregion
    }
}
