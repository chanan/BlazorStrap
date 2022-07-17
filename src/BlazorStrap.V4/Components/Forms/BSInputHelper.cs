using BlazorStrap.Shared.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap.V4.Components.Forms
{
    public class BSInputHelper : BSInputHelperBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.AddContent(0, ChildContent);
        }
    }
}
