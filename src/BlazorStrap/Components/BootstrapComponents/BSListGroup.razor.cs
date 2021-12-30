using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSListGroup : BlazorStrapBase
    {
        [Parameter] public bool IsFlush { get; set; }
        [Parameter] public bool IsHorizontal { get; set; }
        [Parameter] public bool IsNumbered { get; set; }
        [Parameter] public Size Size { get; set; } = Size.None;

        private string? ClassBuilder => new CssBuilder("list-group")
          .AddClass($"list-group-flush", IsFlush)
          .AddClass($"list-group-numbered", IsNumbered)
          .AddClass($"list-group-horizontal", IsHorizontal && Size == Size.None)
          .AddClass($"list-group-horizontal-{Size.ToDescriptionString()}", IsHorizontal && Size != Size.None)
          .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
          .AddClass(Class, !string.IsNullOrEmpty(Class))
          .Build().ToNullString();
    }
}