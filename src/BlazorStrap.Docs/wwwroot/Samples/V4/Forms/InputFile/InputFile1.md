<div class="mb-3">
    <BSLabel>Name HasFile=@Modal.HasFile</BSLabel>
    <BSInputFile IsRequired="true" ValidWhen="@(() => Modal.HasFile)" OnChange="OnFileChange"/>
    <BSFeedback For="() => Modal.HasFile"/>
</div>

@code {
    private ModalClass Modal { get; set; } = new ModalClass();
    private void OnFileChange(InputFileChangeEventArgs e)
    {
        // Resets Validation. Everytime InputFile is clicked it resets.
        Modal.HasFile = null;
        if (e.FileCount > 0)
            Modal.HasFile = true;
    }

    private class ModalClass
    {
        public bool? HasFile { get; set; }
    }
}