using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public class BSButton : BlazorStrapActionBase
    {
        [Parameter] public bool IsReset {
            get => IsResetType;
            set => IsResetType = value;
        }
        [Parameter] public bool IsSubmit{
            get => IsSubmitType;
            set => IsSubmitType = value;
        }
        [Parameter] public bool IsLink{
            get => HasLinkClass;
            set => HasLinkClass = value;
        }
        public BSButton()
        {
            HasButtonClass = true;
        }
        
    }
}
