using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Bootstrap.Base
{
    internal class LayoutBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();

        [Parameter] public RenderFragment? ChildContent { get; set; }

        /// <inheritdoc/>
        [Parameter] public string Class { get; set; } = "";

        /// <inheritdoc/>
        [Parameter] public string DataId { get; set; } = Guid.NewGuid().ToString();

        /// <inheritdoc/>
        [Parameter]
        public Margins Margin { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Margins MarginBottom { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Margins MarginEnd { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Margins MarginLeftAndRight { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Margins MarginStart { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Margins MarginTop { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Margins MarginTopAndBottom { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Padding Padding { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Padding PaddingBottom { get; set; }

        /// <inheritdoc/>

        [Parameter]
        public Padding PaddingEnd { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Padding PaddingLeftAndRight { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Padding PaddingStart { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Padding PaddingTop { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Padding PaddingTopAndBottom { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public Position Position { get; set; } = Position.Default;

        [Parameter]
        public int Version { get; set; }

        public string? LayoutClass => ClassBuilder();

        private string? ClassBuilder()
        {
            if (Version == 4)
            {
                return new CssBuilder()
                   .AddClass($"p-{Padding.ToIndex()}", Padding != Padding.Default)
                   .AddClass($"pt-{PaddingTop.ToIndex()}", PaddingTop != Padding.Default)
                   .AddClass($"pb-{PaddingBottom.ToIndex()}", PaddingBottom != Padding.Default)
                   .AddClass($"pr-{PaddingStart.ToIndex()}", PaddingStart != Padding.Default)
                   .AddClass($"pl-{PaddingEnd.ToIndex()}", PaddingEnd != Padding.Default)
                   .AddClass($"px-{PaddingLeftAndRight.ToIndex()}", PaddingLeftAndRight != Padding.Default)
                   .AddClass($"py-{PaddingTopAndBottom.ToIndex()}", PaddingTopAndBottom != Padding.Default)
                   .AddClass($"m-{Margin.ToIndex()}", Margin != Margins.Default)
                   .AddClass($"mt-{MarginTop.ToIndex()}", MarginTop != Margins.Default)
                   .AddClass($"mb-{MarginBottom.ToIndex()}", MarginBottom != Margins.Default)
                   .AddClass($"mr-{MarginStart.ToIndex()}", MarginStart != Margins.Default)
                   .AddClass($"ml-{MarginEnd.ToIndex()}", MarginEnd != Margins.Default)
                   .AddClass($"mx-{MarginLeftAndRight.ToIndex()}", MarginLeftAndRight != Margins.Default)
                   .AddClass($"my-{MarginTopAndBottom.ToIndex()}", MarginTopAndBottom != Margins.Default)
                   .AddClass($"position-{Position.NameToLower()}", Position != Position.Default)
                   .Build().ToNullString();
            }
            return new CssBuilder()
                .AddClass($"p-{Padding.ToIndex()}", Padding != Padding.Default)
                .AddClass($"pt-{PaddingTop.ToIndex()}", PaddingTop != Padding.Default)
                .AddClass($"pb-{PaddingBottom.ToIndex()}", PaddingBottom != Padding.Default)
                .AddClass($"ps-{PaddingStart.ToIndex()}", PaddingStart != Padding.Default)
                .AddClass($"pe-{PaddingEnd.ToIndex()}", PaddingEnd != Padding.Default)
                .AddClass($"px-{PaddingLeftAndRight.ToIndex()}", PaddingLeftAndRight != Padding.Default)
                .AddClass($"py-{PaddingTopAndBottom.ToIndex()}", PaddingTopAndBottom != Padding.Default)
                .AddClass($"m-{Margin.ToIndex()}", Margin != Margins.Default)
                .AddClass($"mt-{MarginTop.ToIndex()}", MarginTop != Margins.Default)
                .AddClass($"mb-{MarginBottom.ToIndex()}", MarginBottom != Margins.Default)
                .AddClass($"ms-{MarginStart.ToIndex()}", MarginStart != Margins.Default)
                .AddClass($"me-{MarginEnd.ToIndex()}", MarginEnd != Margins.Default)
                .AddClass($"mx-{MarginLeftAndRight.ToIndex()}", MarginLeftAndRight != Margins.Default)
                .AddClass($"my-{MarginTopAndBottom.ToIndex()}", MarginTopAndBottom != Margins.Default)
                .AddClass($"position-{Position.NameToLower()}", Position != Position.Default)
                .Build().ToNullString();
        }
    }
}
