using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public class InteropSyncResult
    {
        public string ClassList { get; set; } = string.Empty;
        public string Styles { get; set; } = string.Empty;
        public bool AriaExpanded { get; set; }
        public bool AriaHidden { get; set; }
        public bool AriaChecked { get; set; }
        public bool AriaDisabled { get; set; }
        public string AriaLabel { get; set; } = string.Empty;
        public string AriaLabelledBy { get; set; } = string.Empty;
        public string AriaDescribedBy { get; set; } = string.Empty; 
        // Add any additional ARIA attributes as needed
    }
}
