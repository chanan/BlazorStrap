using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap
{
    //public class BSContainerBase : LayoutBase
    //{
    //    /// <summary>
    //    /// Sets the container type.
    //    /// </summary>
    //    [Parameter] public Container Container { get; set; } = Container.Default;

    //    protected override void BuildRenderTree(RenderTreeBuilder builder)
    //    {
    //        if (BlazorStrap.BootstrapVersion == BootstrapVersion.Bootstrap4)
    //            Version4RenderBuilder(builder);
    //        else
    //            Version5RenderBuilder(builder);
    //    }
    //    protected override void OnParametersSet()
    //    {
    //        if (BlazorStrap.BootstrapVersion == BootstrapVersion.Bootstrap4 && BlazorStrap.ShowDebugMessages)
    //        {
    //            if (Container == Container.ExtraExtraLarge)
    //                Console.WriteLine("Warning: BSContainer, Bootstrap 4 does not support ExtraExtraLarge. Changed to nearest supported ExrtaLarge");
    //        }

    //        base.OnParametersSet();
    //    }

    //    #region Bootstrap render support methods
    //    protected override string? ClassBuilder()
    //    {
    //        return BlazorStrap.BootstrapVersion == BootstrapVersion.Bootstrap4 ? Version4ClassBuilder() : Version5ClassBuilder();
    //    }

    //    #region Bootstrap 4
    //    protected override string? Version4ClassBuilder()
    //    {
    //        return new CssBuilder()
    //            .AddClass("container", Container == Container.Default)
    //            .AddClass("container-fluid", Container == Container.Fluid)
    //            .AddClass("container-sm", Container == Container.Small)
    //            .AddClass("container-md", Container == Container.Medium)
    //            .AddClass("container-lg", Container == Container.Large)
    //            .AddClass("container-xl", Container == Container.ExrtaLarge)
    //            .AddClass("container-xl", Container == Container.ExtraExtraLarge)
    //            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
    //            .AddClass(Class, !string.IsNullOrEmpty(Class))
    //            .Build().ToNullString();
    //    }
      
    //    protected override void Version4RenderBuilder(RenderTreeBuilder builder)
    //    {
    //        var s = 0;
    //        builder.OpenElement(s, "div");
    //        builder.AddAttribute(s++, "class", ClassBuilder());
    //        builder.AddMultipleAttributes(s++, Attributes);
    //        builder.AddContent(s++, ChildContent);
    //        builder.CloseElement();
    //    }

    //    #endregion

    //    #region Bootstrap 5
    //    protected override string? Version5ClassBuilder()
    //    {
    //        return new CssBuilder()
    //           .AddClass("container", Container == Container.Default)
    //           .AddClass("container-fluid", Container == Container.Fluid)
    //           .AddClass("container-sm", Container == Container.Small)
    //           .AddClass("container-md", Container == Container.Medium)
    //           .AddClass("container-lg", Container == Container.Large)
    //           .AddClass("container-xl", Container == Container.ExrtaLarge)
    //           .AddClass("container-xxl", Container == Container.ExtraExtraLarge)
    //           .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
    //           .AddClass(Class, !string.IsNullOrEmpty(Class))
    //           .Build().ToNullString();
    //    }

    //    protected override void Version5RenderBuilder(RenderTreeBuilder builder)
    //    {
    //        var s = 0;
    //        builder.OpenElement(s, "div");
    //        builder.AddAttribute(s++, "class", ClassBuilder());
    //        builder.AddMultipleAttributes(s++, Attributes);
    //        builder.AddContent(s++, ChildContent);
    //        builder.CloseElement();
    //    }
    //    #endregion
    //    #endregion
    //}
}