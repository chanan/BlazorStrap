using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.DataGrid;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V5.Internal.Do.Not.Use;

public partial class BSDataGridCore<TGridItem> : BSDataGridCoreBase<TGridItem>
{
    /// <summary>
    /// Responsive table size. See <see href="https://getbootstrap.com/docs/5.2/content/tables/#responsive-tables">Bootstrap Documentation</see>.
    /// </summary>
    [Parameter] public Size ResponsiveSize { get; set; } = Size.None;
    protected override string? LayoutClass => LayoutClassBuilder.Build(this);
    protected override string? ClassBuilder => new CssBuilder("table")
        .AddClass("table-striped", IsStriped)
        .AddClass("table-dark", IsDark)
        .AddClass("table-hover", IsHoverable)
        .AddClass("table-sm", IsSmall)
        .AddClass("table-bordered", IsBordered)
        .AddClass("table-borderless", IsBorderLess)
        .AddClass($"table-{Color.NameToLower()}", Color != BSColor.Default)
        .AddClass("caption-top", IsCaptionTop)
        .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
        .AddClass(Class, !string.IsNullOrEmpty(Class))
        .Build().ToNullString();

    protected override string? WrapperClassBuilder => new CssBuilder("bs-table-responsive")
        .AddClass("table-responsive", ResponsiveSize == Size.None)
        .AddClass($"table-responsive-{ResponsiveSize.ToDescriptionString()}", ResponsiveSize != Size.None)
        .AddClass(ResponsiveWrapperClass, !string.IsNullOrEmpty(ResponsiveWrapperClass))
        .Build().ToNullString();
    
    private string? _filterClass => new CssBuilder()
        .AddClass("system-uicons--filter", FilterClass == null)
        .AddClass(FilterClass)
        .Build().ToNullString();
    
    private string? _menuClass => new CssBuilder()
        .AddClass("system-uicons--menu", MenuClass == null)
        .AddClass(MenuClass)
        .Build().ToNullString();
    protected override void OnInitialized()
    {
        
    }

    protected override async Task OnParametersSetAsync()
    {
        Console.WriteLine("This should happend 2nd");
        await base.OnParametersSetAsync();
    }
}