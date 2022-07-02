using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorStrap.Interfaces
{
    internal interface IAlertParameters
    {
        /// <summary>
        /// Color class of alert
        /// </summary>
        BSColor Color { get; set; }

        /// <summary>
        /// Alert body content
        /// </summary>
        RenderFragment? Content { get; set; }

        /// <summary>
        /// Event triggered when alert is dismissed. Only called when <see cref="IsDismissible"/> is true
        /// </summary>
        EventCallback Dismissed { get; set; }

        /// <summary>
        /// Sets whether or not an icon is shown
        /// </summary>
        bool HasIcon { get; set; }

        /// <summary>
        /// Alert header content (optional)
        /// </summary>
        RenderFragment? Header { get; set; }

        /// <summary>
        /// Heading size. Valid values are 1-6
        /// </summary>
        int Heading { get; set; }

        /// <summary>
        /// Determines whether or not an alter is dismissible. See <see cref="Dismissed"/> for the callback
        /// </summary>
        bool IsDismissible { get; set; }
    }
}
