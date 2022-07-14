using BlazorComponentUtilities;

namespace BlazorStrap.V4_6
{
    internal static class LayoutClassBuilder
    {
        internal static string? Build(IBlazorStrapBase blazorStrapBase)
        {
            return new CssBuilder()
                .AddClass($"p-{blazorStrapBase.Padding.ToIndex()}", blazorStrapBase.Padding != Padding.Default)
                .AddClass($"pt-{blazorStrapBase.PaddingTop.ToIndex()}", blazorStrapBase.PaddingTop != Padding.Default)
                .AddClass($"pb-{blazorStrapBase.PaddingBottom.ToIndex()}", blazorStrapBase.PaddingBottom != Padding.Default)
                .AddClass($"pr-{blazorStrapBase.PaddingStart.ToIndex()}", blazorStrapBase.PaddingStart != Padding.Default)
                .AddClass($"pl-{blazorStrapBase.PaddingEnd.ToIndex()}", blazorStrapBase.PaddingEnd != Padding.Default)
                .AddClass($"px-{blazorStrapBase.PaddingLeftAndRight.ToIndex()}", blazorStrapBase.PaddingLeftAndRight != Padding.Default)
                .AddClass($"py-{blazorStrapBase.PaddingTopAndBottom.ToIndex()}", blazorStrapBase.PaddingTopAndBottom != Padding.Default)
                .AddClass($"m-{blazorStrapBase.Margin.ToIndex()}", blazorStrapBase.Margin != Margins.Default)
                .AddClass($"mt-{blazorStrapBase.MarginTop.ToIndex()}", blazorStrapBase.MarginTop != Margins.Default)
                .AddClass($"mb-{blazorStrapBase.MarginBottom.ToIndex()}", blazorStrapBase.MarginBottom != Margins.Default)
                .AddClass($"mr-{blazorStrapBase.MarginStart.ToIndex()}", blazorStrapBase.MarginStart != Margins.Default)
                .AddClass($"ml-{blazorStrapBase.MarginEnd.ToIndex()}", blazorStrapBase.MarginEnd != Margins.Default)
                .AddClass($"mx-{blazorStrapBase.MarginLeftAndRight.ToIndex()}", blazorStrapBase.MarginLeftAndRight != Margins.Default)
                .AddClass($"my-{blazorStrapBase.MarginTopAndBottom.ToIndex()}", blazorStrapBase.MarginTopAndBottom != Margins.Default)
                .AddClass($"position-{blazorStrapBase.Position.NameToLower()}", blazorStrapBase.Position != Position.Default)
                .Build().ToNullString();
        }
    }
}