using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace BlazorStrap.Util.Components
{
    /// <summary>
    /// The base class for BlazorStrap components.
    /// </summary>
    public abstract class BootstrapComponentBase : ComponentBase
    {
        /// <summary>
        /// A dictionary holding any parameter name/value pairs that do not match
        /// properties declared on the component.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }

        /// <inheritdoc />
    }
}
