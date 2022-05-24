using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSContainer : BlazorStrapBase
    {
        /// <summary>
        /// Sets the container type.
        /// </summary>
        [Parameter] public Container Container { get; set; } = Container.Default;

        private string? ClassBuilder => new CssBuilder()
            .AddClass("container", Container == Container.Default)
            .AddClass("container-fluid", Container == Container.Fluid)
            .AddClass("container-sm", Container == Container.Small)
            .AddClass("container-md", Container == Container.Medium)
            .AddClass("container-lg", Container == Container.Large)
            .AddClass("container-xl", Container == Container.ExrtaLarge)
            .AddClass("container-xxl", Container == Container.ExtraExtraLarge)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}
