using Microsoft.AspNetCore.Components;
using System;
using System.Text;

namespace BlazorStrap.Util.Components
{
    public class ColumnBase  : ComponentBase
    {
        internal string GetColumnClass(string defaultClass = "col")
        {
            StringBuilder sb = new StringBuilder();

            AppendProperty(sb, "col-", XS);
            AppendProperty(sb, "order-", XSOrder);
            AppendProperty(sb, "offset-", XSOffset);
            AppendBoolProperty(sb, "mr-auto", MRAuto);
            AppendBoolProperty(sb, "ml-auto", MLAuto);

            AppendProperty(sb, "col-sm-", SM);
            AppendProperty(sb, "order-sm-", SMOrder);
            AppendProperty(sb, "offset-sm-", SMOffset);
            AppendBoolProperty(sb, "mr-sm-auto", SMMRAuto);
            AppendBoolProperty(sb, "ml-sm-auto", SMMLAuto);

            AppendProperty(sb, "col-md-", MD);
            AppendProperty(sb, "order-md-", MDOrder);
            AppendProperty(sb, "offset-md-", MDOffset);
            AppendBoolProperty(sb, "mr-md-auto", MDMRAuto);
            AppendBoolProperty(sb, "ml-md-auto", MDMLAuto);

            AppendProperty(sb, "col-lg-", LG);
            AppendProperty(sb, "order-lg-", LGOrder);
            AppendProperty(sb, "offset-lg-", LGOffset);
            AppendBoolProperty(sb, "mr-lg-auto", LGMRAuto);
            AppendBoolProperty(sb, "ml-lg-auto", LGMLAuto);

            AppendProperty(sb, "col-xl-", XL);
            AppendProperty(sb, "order-xl-", XLOrder);
            AppendProperty(sb, "offset-xl-", XLOffset);
            AppendBoolProperty(sb, "mr-xl-auto", XLMRAuto);
            AppendBoolProperty(sb, "ml-xl-auto", XLMLAuto);

            return sb.Length == 0 ? defaultClass : sb.ToString();
        }

        private void AppendProperty(StringBuilder sb, string prefix, string value)
        {
            if (value != null)
            {
                if (sb.Length != 0)
                {
                    sb.Append(" ");
                }

                sb.Append(prefix + value);
            }
        }

        private void AppendBoolProperty(StringBuilder sb, string value, bool shouldAppend)
        {
            if (shouldAppend)
            {
                if (sb.Length != 0)
                {
                    sb.Append(" ");
                }

                sb.Append(value);
            }
        }

        private string xs;
        [Parameter]
        public string XS
        {
            get => xs;

            set => xs = (int.TryParse(value, out int ivalue), value.ToLowerInvariant()) switch
            {
                (false, "auto") => "auto",
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException("Must be \"auto\" or between 1 and 12", nameof(XS))
            };
        }

        private string sm;
        [Parameter]
        public string SM
        {
            get => sm;

            set => sm = (int.TryParse(value, out int ivalue), value.ToLowerInvariant()) switch
            {
                (false, "auto") => "auto",
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException("Must be \"auto\" or between 1 and 12", nameof(SM))
            };
        }

        private string md;
        [Parameter]
        public string MD
        {
            get => md;

            set => md = (int.TryParse(value, out int ivalue), value.ToLowerInvariant()) switch
            {
                (false, "auto") => "auto",
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException("Must be \"auto\" or between 1 and 12", nameof(MD))
            };
        }

        private string lg;
        [Parameter]
        public string LG
        {
            get => lg;

            set => lg = (int.TryParse(value, out int ivalue), value.ToLowerInvariant()) switch
            {
                (false, "auto") => "auto",
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException("Must be \"auto\" or between 1 and 12", nameof(LG))
            };
        }

        private string xl;
        [Parameter]
        public string XL
        {
            get => xl;

            set => xl = (int.TryParse(value, out int ivalue), value.ToLowerInvariant()) switch
            {
                (false, "auto") => "auto",
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException("Must be \"auto\" or between 1 and 12", nameof(XL))
            };
        }

        private string xsorder;
        [Parameter]
        public string XSOrder
        {
            get => xsorder;

            set => xsorder = (int.TryParse(value, out int ivalue), value.ToLowerInvariant()) switch
            {
                (false, "first") => "first",
                (false, "last") => "last",
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException("Must be \"last\", \"first\", or between 1 and 12", nameof(XSOrder))
            };
        }

        private string smorder;
        [Parameter]
        public string SMOrder
        {
            get => smorder;

            set => smorder = (int.TryParse(value, out int ivalue), value.ToLowerInvariant()) switch
            {
                (false, "first") => "first",
                (false, "last") => "last",
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException("Must be \"last\", \"first\", or between 1 and 12", nameof(SMOrder))
            };
        }

        private string mdorder;
        [Parameter]
        public string MDOrder
        {
            get => mdorder;

            set => mdorder = (int.TryParse(value, out int ivalue), value.ToLowerInvariant()) switch
            {
                (false, "first") => "first",
                (false, "last") => "last",
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException("Must be \"last\", \"first\", or between 1 and 12", nameof(MDOrder))
            };
        }

        private string lgorder;
        [Parameter]
        public string LGOrder
        {
            get => lgorder;

            set => lgorder = (int.TryParse(value, out int ivalue), value.ToLowerInvariant()) switch
            {
                (false, "first") => "first",
                (false, "last") => "last",
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException("Must be \"last\", \"first\", or between 1 and 12", nameof(LGOrder))
            };
        }

        private string xlorder;
        [Parameter]
        public string XLOrder
        {
            get => xlorder;

            set => xlorder = (int.TryParse(value, out int ivalue), value.ToLowerInvariant()) switch
            {
                (false, "first") => "first",
                (false, "last") => "last",
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException("Must be \"last\", \"first\", or between 1 and 12", nameof(XLOrder))
            };
        }

        private string xsoffset;
        [Parameter]
        public string XSOffset
        {
            get => xsoffset;

            set => xsoffset = (int.TryParse(value, out int ivalue), value.ToLowerInvariant()) switch
            {
                (false, _) => throw new ArgumentException("Must be between 1 and 12", nameof(XSOffset)),
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException("Must be between 1 and 12", nameof(XSOffset))
            };
        }

        private string smoffset;
        [Parameter]
        public string SMOffset
        {
            get => smoffset;

            set => smoffset = (int.TryParse(value, out int ivalue), value.ToLowerInvariant()) switch
            {
                (false, _) => throw new ArgumentException("Must be between 1 and 12", nameof(SMOffset)),
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException("Must be between 1 and 12", nameof(SMOffset))
            };
        }

        private string mdoffset;
        [Parameter]
        public string MDOffset
        {
            get => mdoffset;

            set => mdoffset = (int.TryParse(value, out int ivalue), value.ToLowerInvariant()) switch
            {
                (false, _) => throw new ArgumentException("Must be between 1 and 12", nameof(MDOffset)),
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException("Must be between 1 and 12", nameof(MDOffset))
            };
        }

        private string lgoffset;
        [Parameter]
        public string LGOffset
        {
            get => lgoffset;

            set => lgoffset = (int.TryParse(value, out int ivalue), value.ToLowerInvariant()) switch
            {
                (false, _) => throw new ArgumentException("Must be between 1 and 12", nameof(LGOffset)),
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException("Must be between 1 and 12", nameof(LGOffset))
            };
        }

        private string xloffset;
        [Parameter]
        public string XLOffset
        {
            get => xloffset;

            set => xloffset = (int.TryParse(value, out int ivalue), value.ToLowerInvariant()) switch
            {
                (false, _) => throw new ArgumentException("Must be between 1 and 12", nameof(XLOffset)),
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException("Must be between 1 and 12", nameof(XLOffset))
            };
        }

        [Parameter] public bool MRAuto { get; set; }
        [Parameter] public bool MLAuto { get; set; }
        [Parameter] public bool SMMRAuto { get; set; }
        [Parameter] public bool SMMLAuto { get; set; }
        [Parameter] public bool MDMRAuto { get; set; }
        [Parameter] public bool MDMLAuto { get; set; }
        [Parameter] public bool LGMRAuto { get; set; }
        [Parameter] public bool LGMLAuto { get; set; }
        [Parameter] public bool XLMRAuto { get; set; }
        [Parameter] public bool XLMLAuto { get; set; }
    }
}
