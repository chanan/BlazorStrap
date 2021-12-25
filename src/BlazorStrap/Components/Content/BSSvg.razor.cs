using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public partial class BSSvg : BlazorStrapBase
    {
        [Inject] public ISvgLoader SvgLoader { get; set; }
        [Parameter] public string Source { get; set; }

        protected MarkupString Markup { get; set; }
        private string? ClassBuilder => new CssBuilder()
          .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
          .AddClass(Class, !string.IsNullOrEmpty(Class))
          .Build().ToNullString();


        
        protected override async Task OnInitializedAsync()
        {
            if(!string.IsNullOrWhiteSpace(Source))
            {
                Markup = await SvgLoader.LoadSvg(Source);
            }
        }

    }
}
