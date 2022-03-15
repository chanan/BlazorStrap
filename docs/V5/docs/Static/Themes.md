## Theme Switcher

BlazorStrap is capable of dynamically loading in themes. For the moment we supply only the https://bootswatch.com themes.
If requested we will supply a way to change out the paths on the Theme Switcher.

### Example
You can view the full code for this by visiting our GitHub repo.

``` C#
private string? Selected { get; set; }
private List<string> _themes = new List<string>();

    protected override void OnInitialized()
    {
        _themes = Enum.GetNames(typeof(Theme)).ToList();
    }

    protected override async Task OnAfterRenderAsync(bool firstrun)
    {
        if (firstrun)
        {
            await _blazorStrap.SetBootstrapCss("5.1.3");
        }
    }
    private async Task SelectedChanged(string value)
    {
        Selected = value;
        await _blazorStrap.SetBootstrapCss(value, "5.1.3");
    }
}
```