using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSTR : BlazorStrapBase
    {
        [Parameter] public AlignRow AlignRow { get; set; }
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;
        [Parameter] public bool IsActive { get; set; }

        private string? ClassBuilder => new CssBuilder()
          .AddClass("table-active", IsActive)
          .AddClass($"table-{BSColor.GetName<BSColor>(Color).ToLower()}", Color != BSColor.Default)
          .AddClass($"align-{AlignRow.GetName<AlignRow>(AlignRow).ToLower()}", AlignRow != AlignRow.Default)
          .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
          .AddClass(Class, !string.IsNullOrEmpty(Class))
          .Build().ToNullString();
    }
}
