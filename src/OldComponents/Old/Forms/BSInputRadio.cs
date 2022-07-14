using System.Diagnostics.CodeAnalysis;
using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{

    public class BSInputRadio<T> : BSInputCheckbox<T>
    {
        public BSInputRadio()
        {
            IsRadio = true;
        }
    }
}