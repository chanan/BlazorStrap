using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorStrap.Bootstrap.Interfaces
{
    internal interface IBase
    {
        IDictionary<string, object> Attributes { get; set; }
        RenderFragment? ChildContent { get; set; }
        string DataId { get; set; }
        string? Class { get; set; }
        string? LayoutClass { get; set; }
        Margins Margin { get; set; }
        Margins MarginBottom { get; set; }
        Margins MarginEnd { get; set; }
        Margins MarginLeftAndRight { get; set; }
        Margins MarginStart { get; set; }
        Margins MarginTop { get; set; }
        Margins MarginTopAndBottom { get; set; }
        Padding Padding { get; set; }
        Padding PaddingBottom { get; set; }
        Padding PaddingEnd { get; set; }
        Padding PaddingLeftAndRight { get; set; }
        Padding PaddingStart { get; set; }
        Padding PaddingTop { get; set; }
        Padding PaddingTopAndBottom { get; set; }
        Position Position { get; set; }
    }
}
