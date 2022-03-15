using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSTD : BlazorStrapBase
    {
        [Parameter] public AlignRow AlignRow { get; set; }
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;
        [Parameter] public string? ColSpan { get; set; }
        [Parameter] public bool IsActive { get; set; }

        internal string? ClassBuilder => new CssBuilder()
          .AddClass("table-active", IsActive)
          .AddClass($"table-{Color.NameToLower()}", Color != BSColor.Default)
          .AddClass($"align-{AlignRow.NameToLower()}", AlignRow != AlignRow.Default)
          .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
          .AddClass(Class, !string.IsNullOrEmpty(Class))
          .Build().ToNullString();

        [CascadingParameter] internal BSTHead? TableHead { get; set; }
    }
}
