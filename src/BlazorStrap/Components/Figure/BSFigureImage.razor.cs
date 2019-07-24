using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSFigureImage : BootstrapComponentBase
    {
        protected string classname =>
        new CssBuilder("figure-img img-fluid rounded")
            .AddClass(Class)
            .Build();

        [Parameter] protected string Class { get; set; }
        [Parameter] protected string Style { get; set; }
    }
}
