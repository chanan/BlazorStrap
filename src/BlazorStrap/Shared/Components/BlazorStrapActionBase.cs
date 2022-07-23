using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap.Shared.Components
{
    public abstract class BlazorStrapActionBase<TSize> : BlazorStrapBase where TSize : Enum
    {
        protected bool IsLinkType { get; init; }
        protected bool HasButtonClass { get; set; }
        protected bool HasLinkClass { get; set; }
        protected bool IsResetType { get; set; }
        protected bool IsSubmitType { get; set; }
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;
        [Parameter] public TSize Size { get; set; } = (TSize)(object)0;
        [Parameter] public bool? IsActive { get; set; }
        [Parameter] public bool IsDisabled { get; set; }
        [Parameter] public bool IsOutlined { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public string? Target { get; set; }
        protected string? UrlBase { get; set; }

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }

        protected string ButtonType()
        {
            return IsSubmitType ? "submit" : IsResetType ? "reset" : "button";
        }

        protected async Task ClickEvent(MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(Target))
                BlazorStrapService.ForwardClick(Target);
            if (OnClick.HasDelegate)
                await OnClick.InvokeAsync(e);
        }

        protected string GetTag => IsLinkType ? "a" : "button";

        
    }
}
