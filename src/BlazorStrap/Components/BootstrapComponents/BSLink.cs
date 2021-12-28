using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public class BSLink : BlazorStrapActionBase
    {
        [Parameter] public bool IsReset {
            get => IsResetType;
            set => IsResetType = value;
        }
        [Parameter] public bool IsSubmit{
            get => IsSubmitType;
            set => IsSubmitType = value;
        }
        [Parameter] public bool IsButton{
            get => HasButtonClass;
            set => HasButtonClass = value;
        }
        [Parameter] public string? Url{
            get => UrlBase;
            set => UrlBase = value;
        }
        public BSLink()
        {
            IsLinkType = true;
        }
        
    }
}
