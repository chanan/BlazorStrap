using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap;

public class BlazorStrapActionBase : BlazorStrapBase
{
    protected bool IsLinkType { get; set; } = false;
    protected bool HasButtonClass { get; set; } = false;
    protected bool HasLinkClass { get; set; } = false;
    protected bool IsResetType { get; set; }
    protected bool IsSubmitType { get; set; }
    [Parameter] public BSColor Color { get; set; } = BSColor.Default;
    [Parameter] public bool IsActive { get; set; }
    [Parameter] public bool IsDisabled { get; set; }
    [Parameter] public bool IsOutlined { get; set; }

    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
    [Parameter] public Size Size { get; set; } = Size.None;
    [Parameter] public string? Target { get; set; }
    protected string? UrlBase { get; set; }

    private string? ClassBuilder => new CssBuilder()
        .AddClass("btn", !IsLinkType || HasButtonClass)
        .AddClass($"btn-outline-{Color.NameToLower()}", IsOutlined && (!IsLinkType || HasButtonClass))
        .AddClass($"btn-{Color.NameToLower()}",
            Color != BSColor.Default && !IsOutlined && (!IsLinkType || HasButtonClass))
        .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None && (!IsLinkType || HasButtonClass))
        .AddClass("btn-link", HasLinkClass)
        .AddClass("active", IsActive)
        .AddClass("disabled", IsDisabled)
        .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
        .AddClass(Class, !string.IsNullOrEmpty(Class))
        .Build().ToNullString();

    private string ButtonType()
    {
        if (IsSubmitType) return "submit";
        return IsResetType ? "reset" : "button";
    }

    private async Task ClickEvent(MouseEventArgs e)
    {
        if (!string.IsNullOrEmpty(Target))
            BlazorStrap.OnForwardClick(Target);
        await OnClick.InvokeAsync(e);
    }

    private string GetTag => IsLinkType ? "a" : "button";

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, GetTag);
        if (!IsLinkType)
            builder.AddAttribute(1, "type", ButtonType());
        builder.AddAttribute(2, "class", ClassBuilder);
        builder.AddAttribute(3, "data-blazorstrap", DataId);
        if (!string.IsNullOrEmpty(Target))
        {
            builder.AddAttribute(4, "data-blazorstrap-target", Target);
            if (IsLinkType)
                builder.AddAttribute(5, "href", "javascript:void(0);");
        }
        else
        {
            if (IsLinkType)
                builder.AddAttribute(6, "href", UrlBase);
        }

        builder.AddAttribute(7, "onclick", EventCallback.Factory.Create(this, ClickEvent));
        builder.AddMultipleAttributes(8, Attributes);
        builder.AddContent(9, @ChildContent);
        builder.CloseElement();
    }
}