using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorStrap.Interfaces
{
   public interface IBSDropdown
    {
        /// <summary>
        /// Clicking inside the dropdown menu will not close it.
        /// </summary>
        bool AllowItemClick { get; set; }

        /// <summary>
        /// Clicks outside of the dropdown will not cause the dropdown to close.
        /// </summary>
        bool AllowOutsideClick { get; set; }

        /// <summary>
        /// Dropdown menu content.
        /// </summary>
        RenderFragment? Content { get; set; }

        /// <summary>
        /// Hides the dropdown button and only shows the content.
        /// </summary>
        bool Demo { get; set; }

        /// <summary>
        /// Adds the <c>dropdown-menu-dark</c> css class making the dropdown content dark.
        /// </summary>
        bool IsDark { get; set; }

        /// <summary>
        /// Renders the dropdown menu with a <c>div</c> and uses popper.js to create.
        /// </summary>
        bool IsDiv { get; set; }

        /// <summary>
        /// A combination of <see cref="AllowItemClick"/> and <see cref="AllowOutsideClick"/>.
        /// Requires the dropdown to be closed by clicking the button again.
        /// </summary>
        bool IsManual { get; set; }

        /// <summary>
        /// Renders dropdown as a <see cref="BSPopover"/> element and sets <see cref="BSPopover.IsNavItemList"/> true.
        /// </summary>
        bool IsNavPopper { get; set; }

        /// <summary>
        /// Disables dynamic positioning.
        /// </summary>
        bool IsStatic { get; set; }

        /// <summary>
        /// Dropdown offset.
        /// </summary>
        string? Offset { get; set; }

        /// <summary>
        /// Dropdown placement.
        /// </summary>
        Placement Placement { get; set; }

        /// <summary>
        /// Attribute to add when dropdown is shown.
        /// </summary>
        string? ShownAttribute { get; set; }

        /// <summary>
        /// data-blazorstrap data Id of target element
        /// </summary>
        string Target { get; set; }

        /// <summary>
        /// Element to be used to toggle the dropdown.
        /// </summary>
        RenderFragment? Toggler { get; set; }

        bool Shown { get; }
        IBSNavItem? DropdownItem { get; set; }
        IBSButtonGroup? Group { get; set; }
        IBSInputGroup? InputGroup { get; set; }
        IBSNavItem? NavItem { get; set; }
        IBSDropdown? Parent { get; set; }
        Action<bool, IBSDropdownItem>? OnSetActive { get; set; }
        string DataId { get; }
        bool Active { get; }

        Task ToggleAsync();

    }
}
