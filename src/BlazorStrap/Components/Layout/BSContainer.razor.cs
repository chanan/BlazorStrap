using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSContainer : BlazorStrapBase
    {
        [Parameter] public Container Container { get; set; } = new Container();

        internal string? ClassBuilder => new CssBuilder()
            .AddClass("container", Container.HasFlag(Container.Default))
            .AddClass("container-fluid", Container.HasFlag(Container.Fluid))
            .AddClass("container-sm", Container.HasFlag(Container.Small))
            .AddClass("container-md", Container.HasFlag(Container.Medium))
            .AddClass("container-lg", Container.HasFlag(Container.Large))
            .AddClass("container-xl", Container.HasFlag(Container.ExrtaLarge))
            .AddClass("container-xxl", Container.HasFlag(Container.ExtraExtraLarge))
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}
