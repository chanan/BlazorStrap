<BSButton Color="BSColor.Primary" @ref="Control">Primary</BSButton>
<BSButton Color="BSColor.Secondary">Primary</BSButton>
<BSButton Color="BSColor.Success">Success</BSButton>
<BSButton Color="BSColor.Danger">Danger</BSButton>
<BSButton Color="BSColor.Warning">Warning</BSButton>
<BSButton Color="BSColor.Info">Info</BSButton>
<BSButton Color="BSColor.Light">Light</BSButton>
<BSButton Color="BSColor.Dark">Dark</BSButton>
<BSButton IsLink="true">Posing as link</BSButton>


@code {
    public BSButton? Control { get; set; } = default!;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        // set focus
        if (Control.Element != null)
            await Control.Element.Value.FocusAsync(); // not found
    }
}