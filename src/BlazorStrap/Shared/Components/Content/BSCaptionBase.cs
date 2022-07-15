using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorStrap.Shared.Components.Content
{
    public abstract class BSCaptionBase : BlazorStrapBase
    {
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
    }
}
