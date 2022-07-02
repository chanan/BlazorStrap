using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorStrap.Interfaces
{
    internal interface ICommonParameters
    {
        IDictionary<string, object> Attributes { get; set; }
        RenderFragment? ChildContent { get; set; }
        string DataId { get; set; }
        string? Class { get; set; }
        string? LayoutClass { get; set; }
    }
}
