## Theme Switcher

BlazorStrap is capable of dynamically loading in themes. For the moment we supply only the https://bootswatch.com themes.

### Additional Details.
The Theme Switcher operates via javascript it will load in from two paths depending on the theme name.
1. If only the version is given and/or the theme name is bootstrap 
   "https://cdn.jsdelivr.net/npm/bootstrap@" + version + "/dist/css/bootstrap.min.css" 
2. The theme name is anything other then bootstrap
   "https://cdn.jsdelivr.net/npm/bootswatch@" + version + "/dist/" + theme + "/bootstrap.min.css";

Additionally the theme switcher is capable of replacing any existing reference to bootstrap.min.css this is done to prevent flickering.

A list of themes are stored in the Theme Enum. In the example below we read out all themes in that Enum and store them in a list.
However, you can reference them by name see https://bootswatch.com themes.

### SetBootstrapCss Method
   Has two overloads.
   1) Task SetBootstrapCss(string version)
      This overload loads the default bootstrap.min.css from the cdn by the given version number.
   2) Task SetBootstrapCss(string? theme, string version)
      This overload loads the bootswatch version of bootstrap.min.css from the cdn by the given theme name and version number.

### Usage 
   1) @inject IBlazorStrap _blazorStrap
   2) Call `await _blazorStrap.SetBootstrapCss(params);` from async method to set the current theme.
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

#### Advanced 
In Javascript you can replace SetBootstrapCss to create custom theme loader. This will need to be loaded in after blazorstrap.js
``` javascript
let link;
window.blazorStrap.SetBootstrapCss = function (theme, version) {
// Your code for modifying the header
// Helpful Snippet
 if (link === undefined) {
            let existing = document.querySelectorAll('link[href$="bootstrap.min.css"]')[0];

            if (existing === undefined) {
                link = document.createElement('link');
                document.head.insertBefore(link, document.head.firstChild);
                link.type = 'text/css';
                link.rel = 'stylesheet';
            } else link = existing;
        }

// Required 
return true;
};
```