<BSModal DataId="modal1">
    <Header>Modal Title</Header>
    <Content>Woohoo, you're reading this text in a modal!</Content>
    <Footer Context="modal">
        <BSButton MarginStart="Margins.Auto" Color="BSColor.Secondary" @onclick="modal.HideAsync">Close</BSButton>
        <BSButton Color="BSColor.Primary">Save changes</BSButton>
    </Footer>
</BSModal>
<BSButton Color="BSColor.Primary" Target="modal1">Launch demo modal</BSButton>

<div>This will be removed however is here to show the improments of the interop. The unsafe would have had a high chance of breaking before.</div>
<BSModal @ref="_spamModal">
    <Header>Modal Title</Header>
    <Content>Woohoo, you're reading this text in a modal!</Content>
    <Footer Context="modal">
        <BSButton MarginStart="Margins.Auto" Color="BSColor.Secondary" @onclick="modal.HideAsync">Close</BSButton>
        <BSButton Color="BSColor.Primary">Save changes</BSButton>
    </Footer>
</BSModal>

<BSButton Color="BSColor.Warning" OnClick="SpamModelAsync" >Spam demo modal</BSButton>
<BSButton Color="BSColor.Danger" OnClick="UnSafeSpamModelAsync">Spam the unsafe way</BSButton>

@code{
    private BSModal? _spamModal;
    private async Task SpamModelAsync()
    {
        if(_spamModal != null)
        {
            for(var i =0; i < 50; ++i)
            {
                await _spamModal.ToggleAsync();
                Console.WriteLine($"Safe Spam {i}");
                Console.WriteLine($"Shown: {_spamModal.Shown}");
            }
        }
    }
    private async Task UnSafeSpamModelAsync()
    {
        if (_spamModal != null)
        {
            for (var i = 0; i < 50; ++i)
            {
                _= _spamModal.ToggleAsync();
                Console.WriteLine($"Unsafe Spam {i}");
                Console.WriteLine($"Shown: {_spamModal.Shown}");
            }
        }
    }
}