using Microsoft.AspNetCore.Components;
using System;
using System.Text;

namespace BlazorStrap.Util
{
    public class ColumnBase : BootstrapComponentBase
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
        protected string XS
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
        protected string SM
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
        protected string MD
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
        protected string LG
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
        protected string XL
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
        protected string XSOrder
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
        protected string SMOrder
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
        protected string MDOrder
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
        protected string LGOrder
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
        protected string XLOrder
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
        protected string XSOffset
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
        protected string SMOffset
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
        protected string MDOffset
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
        protected string LGOffset
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
        protected string XLOffset
        {
            get => xloffset;

            set => xloffset = (int.TryParse(value, out int ivalue), value.ToLowerInvariant()) switch
            {
                (false, _) => throw new ArgumentException("Must be between 1 and 12", nameof(XLOffset)),
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException("Must be between 1 and 12", nameof(XLOffset))
            };
        }

        [Parameter] protected bool MRAuto { get; set; }
        [Parameter] protected bool MLAuto { get; set; }
        [Parameter] protected bool SMMRAuto { get; set; }
        [Parameter] protected bool SMMLAuto { get; set; }
        [Parameter] protected bool MDMRAuto { get; set; }
        [Parameter] protected bool MDMLAuto { get; set; }
        [Parameter] protected bool LGMRAuto { get; set; }
        [Parameter] protected bool LGMLAuto { get; set; }
        [Parameter] protected bool XLMRAuto { get; set; }
        [Parameter] protected bool XLMLAuto { get; set; }
    }
}
