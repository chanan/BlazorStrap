using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSCustomizer : ComponentBase
    {
        [Parameter] public BSCustomizerConfig Config { get; set; } = new BSCustomizerConfig();
    }
    public class BSCustomizerConfig
    {
        public string Blue { get; set; } = "";
        public string Indigo { get; set; }
        public string Purple { get; set; }
        public string Pink { get; set; }
        public string Red { get; set; }
        public string Orange { get; set; }
        public string Yellow { get; set; }
        public string Green { get; set; }
        public string Teal { get; set; }
        public string Cyan { get; set; }
        public string White { get; set; }
        public string Gray { get; set; }
        public string GrayDark { get; set; }
        public string Primary { get; set; }
        public string Secondary { get; set; }
        public string Success { get; set; }
        public string Info { get; set; }
        public string Warning { get; set; }
        public string Danger { get; set; }
        public string Light { get; set; }
        public string Dark { get; set; }
        public string BreakpointXS { get; set; }
        public string BreakpointSM { get; set; }
        public string BreakpointMS { get; set; }
        public string BreakpointLG { get; set; }
        public string BreakpointXL { get; set; }
        public string FontFamilySansSerif { get; set; }
        public string FontFamilyMonospace { get; set; }

    }
}