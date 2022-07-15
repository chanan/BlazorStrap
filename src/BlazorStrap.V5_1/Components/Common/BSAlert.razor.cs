using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;
using BlazorStrap.Utilities;

namespace BlazorStrap.V5_1
{
    public partial class BSAlert : BSAlertBase
    {
        private bool _dismissed;
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("alert")
                .AddClass($"alert-{Color.NameToLower()}", Color != BSColor.Default)
                .AddClass("d-flex align-items-center", HasIcon)
                .AddClass("alert-dismissible", IsDismissible)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();

        /// <summary>
        /// Dismisses the alert
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        public override async Task CloseEventAsync()
        {
            await EventUtil.AsNonRenderingEventHandler(Dismissed.InvokeAsync).Invoke();
            _dismissed = true;
        }

        /// <summary>
        /// Opens the alert
        /// </summary>
        public override void Open()
        {
            _dismissed = false;
        }
    }
}
