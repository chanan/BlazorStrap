@using BlazorStrap.Extensions.Wizard
@inject IBlazorStrap BlazorStrap
<BSToaster/>
<BSWizard OnError="OnError">
    <ChildContent>
        <BSWizardItem>
            <Label>Step 1</Label>
            <Content>Wizard 1</Content>
        </BSWizardItem>
        <BSWizardItem>
            <Label>Step 2</Label>
            <Content>Wizard 2</Content>
        </BSWizardItem>
        <BSWizardItem>
            <Label>Step 3</Label>
            <Content>Wizard 3</Content>
        </BSWizardItem>
        <BSWizardItem>
            <Label>Step 4</Label>
            <Content>Wizard 4</Content>
        </BSWizardItem>
    </ChildContent>
    <BackButton>
        @if (context.Children.IndexOf(context?.ActiveChild ?? new BSWizardItem()) > 0)
        {
            <BSButton OnClick="() => BackAsync(context)">Back</BSButton>
        }
    </BackButton>
    <NextButton>
        @if (context.Children.IndexOf(context?.ActiveChild ?? new BSWizardItem()) < context.Children.Count)
        {
            <BSButton OnClick="() => NextAsync(context)">Next</BSButton>
        }
    </NextButton>
</BSWizard>

@code {

    private async Task NextAsync(BSWizard wizard)
    {
        if (wizard.ActiveChild == null) return;
        var next = wizard.Children.IndexOf(wizard.ActiveChild);
        if (next < wizard.Children.Count -1)
        {
            // Don't do this here. Demo only
            wizard.ActiveChild.IsDone = true;
            await wizard.InvokeAsync(wizard.Children[next + 1]);
        }
    }

    private async Task BackAsync(BSWizard wizard)
    {
        if (wizard.ActiveChild == null) return;
        var back = wizard.Children.IndexOf(wizard.ActiveChild);
        if (back > 0)
        {
            // Don't do this here. Demo only
            wizard.ActiveChild.IsDone = false;
            await wizard.InvokeAsync(wizard.Children[back - 1]);
        }
    }

    private void OnError(string error)
    {
        BlazorStrap.Toaster.Add("Error" , error , options =>
        {
            options.Color = BSColor.Danger;
            options.CloseAfter = 5000;
        });
    }

}