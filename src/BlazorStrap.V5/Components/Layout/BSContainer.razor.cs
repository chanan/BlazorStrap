using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Layout;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V5
{
    public partial class BSContainer : BSContainerBase
    {
        /// <summary>
        /// Sets the container type.
        /// </summary>
        [Parameter] public Container Container { get; set; } = Container.Default;

        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
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