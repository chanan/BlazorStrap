﻿using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSFormLabel : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder()
            .AddClass(Class)
        .Build();

        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}