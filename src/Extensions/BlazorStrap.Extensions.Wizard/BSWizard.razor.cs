using System.Reflection;
using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Extensions.Wizard.Extensions;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Extensions.Wizard;

public partial class BSWizard : ComponentBase
{
    [Parameter] public string? Class { get; set; } = "nav-fill";
    [Parameter] public string? CardClass { get; set; }
    [Parameter] public string? CardHeaderClass { get; set; }
    [Parameter] public string? CardBodyClass { get; set; }
    [Parameter] public string? CardBodyFooterClass { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public RenderFragment<BSWizard>? NextButton { get; set; }
    [Parameter] public RenderFragment<BSWizard>? BackButton { get; set; }
    [Parameter] public EventCallback<string> OnError { get; set; }
    private BSWizardRender? _wizardRender;
    public List<BSWizardItem> Children { get; set; } = new List<BSWizardItem>(); 
    protected string? ClassBuilder => new CssBuilder()
        
        .AddClass("nav")
        .AddClass("nav-pills", IsPills)
        .AddClass(Class, !string.IsNullOrEmpty(Class))
        .Build().ToNullString();
    
        /// <summary>
        /// Adds the <c>nav-pills</c> class to render as pills.
        /// </summary>
        [Parameter] public bool IsPills { get; set; }
        
        public BSWizardItem? ActiveChild { get; set; }

        internal bool HasTabError { get; set; }

        public void AddChild(BSWizardItem sender)
        {
            if(Children.All(q => q != sender))
                Children.Add(sender);
        }
        public void RemoveChild(BSWizardItem sender)
        {
            if(Children.Any(q => q == sender))
                Children.Remove(sender);
        }
        public bool SetFirstChild(BSWizardItem sender)
        {
            if (ActiveChild != null) return false;
            ActiveChild = sender;
            
            ChildHandler?.Invoke(sender);
            _wizardRender?.Refresh();
            return true;
        }

        public async Task InvokeAsync(BSWizardItem sender)
        {
            ActiveChild = sender;
            ChildHandler?.Invoke(sender);
            if (_wizardRender != null) await _wizardRender.RefreshAsync();
        }

        public event Action<BSWizardItem>? ChildHandler;
        
}
