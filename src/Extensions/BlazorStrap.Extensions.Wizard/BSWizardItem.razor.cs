using System.Reflection;
using BlazorComponentUtilities;
using BlazorStrap.Extensions.Wizard.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap.Extensions.Wizard;

public partial class BSWizardItem : ComponentBase, IDisposable
{
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();

    // ReSharper disable once NullableWarningSuppressionIsUsed
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Parameter] public string Class { get; set; } = string.Empty;
    [Parameter] public string DoneClass { get; set; } = string.Empty;
    /// <summary>
    /// Data-Blazorstrap attribute value to target.
    /// </summary>
    [Parameter]
    public string? Target { get; set; }

    /// <summary>
    /// Sets if the NavItem is active.
    /// </summary>
    [Parameter]
    public bool? IsActive { get; set; }

    /// <summary>
    /// Sets if the NavItem is disabled.
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// Sets if the NavItem is a dropdown.
    /// </summary>
    [Parameter]
    public bool IsDropdown { get; set; }

    /// <summary>
    /// Removes the <c>nav-item</c> class.
    /// </summary>
    [Parameter]
    public bool NoNavItem { get; set; }

    /// <summary>
    /// Display nav item as active if a child route of the nav item is active.
    /// </summary>
    [Parameter]
    public bool ActiveOnChildRoutes { get; set; } = false;

    /// <summary>
    /// CSS class to apply to the nav bar list items.
    /// </summary>
    [Parameter]
    public string? ListItemClass { get; set; }

    [Parameter] public string? DoneListItemClass { get; set; } = "wizard-item-done";

    /// <summary>
    /// Event called when item is clicked.
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    /// <summary>
    /// Prevent default on click behavior.
    /// </summary>
    [Parameter]
    public bool PreventDefault { get; set; }

    /// <summary>
    /// Content .
    /// </summary>
    [Parameter]
    public RenderFragment<BSWizard>? Content { get; set; }

    /// <summary>
    /// Label.
    /// </summary>
    [Parameter]
    public RenderFragment? Label { get; set; }

    /// <summary>
    /// Url for nav link.
    /// </summary>
    [Parameter]
    public string? Url { get; set; } = "javascript:void(0);";

    [Parameter] public bool HasError { get; set; }
    [Parameter] public bool IsDone { get; set; }
    [CascadingParameter] public BSWizard? Parent { get; set; }
    private bool _canHandleActive;

    protected override void OnInitialized()
    {
        if(Parent != null)
            Parent.AddChild(this);
        if (IsActive == null)
        {
            _canHandleActive = true;
            if (NavigationManager.Uri == NavigationManager.BaseUri + Url?.TrimStart('/'))
                IsActive = true;
            if (NavigationManager.Uri.Contains(NavigationManager.BaseUri + Url?.TrimStart('/')) && ActiveOnChildRoutes)
                IsActive = true;
        }
    }


    protected override void OnParametersSet()
    {
        if (Parent == null) return;

        if (Parent.ActiveChild == null)
            IsActive = Parent.SetFirstChild(this);

        Parent.ChildHandler += Parent_ChildHandler;
    }

    protected async Task ClickEvent()
    {
        if(Parent?.ActiveChild == null ) return;
        if (!Parent.ActiveChild.IsDone && Parent.Children.IndexOf(this) > Parent.Children.IndexOf(Parent.ActiveChild))
        {
            if (Parent.OnError.HasDelegate)
                await Parent.OnError.InvokeAsync("Previous step is not complete");
            return;
        }

        if (OnClick.HasDelegate)
            await OnClick.InvokeAsync();

        Parent?.InvokeAsync(this);
    }

    private async void Parent_ChildHandler(BSWizardItem sender)
    {
        if (Parent != null)
            IsActive = Parent.ActiveChild == this;
        await InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        if (Parent == null) return;
        
        Parent.RemoveChild(this);
        if (Parent.ActiveChild == this)
            Parent.ActiveChild = null;
        Parent.ChildHandler -= Parent_ChildHandler;
    }

    protected string? ClassBuilder => new CssBuilder("nav-link")
        .AddClass("active", IsActive ?? false)
        .AddClass("disabled", IsDisabled)
        .AddClass(Class, !string.IsNullOrEmpty(Class))
        .AddClass(DoneClass, !string.IsNullOrEmpty(DoneClass) && IsDone)
        .AddClass("invalid", !string.IsNullOrEmpty(DoneClass) && HasError)
        .Build().ToNullString();

    protected string? ListClassBuilder => new CssBuilder()
        .AddClass("nav-item", !NoNavItem)
        .AddClass("dropdown", IsDropdown)
        .AddClass(ListItemClass)
        .AddClass(DoneListItemClass, !string.IsNullOrEmpty(DoneListItemClass) && IsDone)
        .AddClass("invalid", !string.IsNullOrEmpty(DoneListItemClass) && HasError)
        .Build().ToNullString();
}