using Microsoft.AspNetCore.Components;
using System;
using System.Text;

namespace BlazorStrap.Util.Components
{
    public abstract class ColumnBase : ComponentBase
    {
        internal string GetColumnClass(string defaultClass = "col")
        {
            var sb = new StringBuilder();

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

        private static void AppendProperty(StringBuilder sb, string prefix, string value)
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

        private static void AppendBoolProperty(StringBuilder sb, string value, bool shouldAppend)
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

        private string _xs;
        [Parameter]
        public string XS
        {
            get => _xs;

            set => _xs = (int.TryParse(value, out var ivalue), value?.ToLowerInvariant()) switch
            {
                (false, "auto") => "auto",
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException(Properties.Resources.Between_1_and_12_Auto, nameof(XS))
            };
        }

        private string _sm;
        [Parameter]
        public string SM
        {
            get => _sm;

            set => _sm = (int.TryParse(value, out var ivalue), value?.ToLowerInvariant()) switch
            {
                (false, "auto") => "auto",
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException(Properties.Resources.Between_1_and_12_Auto, nameof(SM))
            };
        }

        private string _md;
        [Parameter]
        public string MD
        {
            get => _md;

            set => _md = (int.TryParse(value, out var ivalue), value?.ToLowerInvariant()) switch
            {
                (false, "auto") => "auto",
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException(Properties.Resources.Between_1_and_12_Auto, nameof(MD))
            };
        }

        private string _lg;
        [Parameter]
        public string LG
        {
            get => _lg;

            set => _lg = (int.TryParse(value, out var ivalue), value?.ToLowerInvariant()) switch
            {
                (false, "auto") => "auto",
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException(Properties.Resources.Between_1_and_12_Auto, nameof(LG))
            };
        }

        private string _xl;
        [Parameter]
        public string XL
        {
            get => _xl;

            set => _xl = (int.TryParse(value, out var ivalue), value?.ToLowerInvariant()) switch
            {
                (false, "auto") => "auto",
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException(Properties.Resources.Between_1_and_12_Auto, nameof(XL))
            };
        }

        private string _xsorder;
        [Parameter]
        public string XSOrder
        {
            get => _xsorder;

            set => _xsorder = (int.TryParse(value, out var ivalue), value?.ToLowerInvariant()) switch
            {
                (false, "first") => "first",
                (false, "last") => "last",
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException(Properties.Resources.Between_1_and_12_Last_First, nameof(XSOrder))
            };
        }

        private string _smorder;
        [Parameter]
        public string SMOrder
        {
            get => _smorder;

            set => _smorder = (int.TryParse(value, out var ivalue), value?.ToLowerInvariant()) switch
            {
                (false, "first") => "first",
                (false, "last") => "last",
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException(Properties.Resources.Between_1_and_12_Last_First, nameof(SMOrder))
            };
        }

        private string _mdorder;
        [Parameter]
        public string MDOrder
        {
            get => _mdorder;

            set => _mdorder = (int.TryParse(value, out var ivalue), value?.ToLowerInvariant()) switch
            {
                (false, "first") => "first",
                (false, "last") => "last",
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException(Properties.Resources.Between_1_and_12_Last_First, nameof(MDOrder))
            };
        }

        private string _lgorder;
        [Parameter]
        public string LGOrder
        {
            get => _lgorder;

            set => _lgorder = (int.TryParse(value, out var ivalue), value?.ToLowerInvariant()) switch
            {
                (false, "first") => "first",
                (false, "last") => "last",
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException(Properties.Resources.Between_1_and_12_Last_First, nameof(LGOrder))
            };
        }

        private string _xlorder;
        [Parameter]
        public string XLOrder
        {
            get => _xlorder;

            set => _xlorder = (int.TryParse(value, out var ivalue), value?.ToLowerInvariant()) switch
            {
                (false, "first") => "first",
                (false, "last") => "last",
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException(Properties.Resources.Between_1_and_12_Last_First, nameof(XLOrder))
            };
        }

        private string _xsoffset;
        [Parameter]
        public string XSOffset
        {
            get => _xsoffset;

            set => _xsoffset = (int.TryParse(value, out var ivalue), value?.ToLowerInvariant()) switch
            {
                (false, _) => throw new ArgumentException(Properties.Resources.Between_1_and_12, nameof(XSOffset)),
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException(Properties.Resources.Between_1_and_12, nameof(XSOffset))
            };
        }

        private string _smoffset;
        [Parameter]
        public string SMOffset
        {
            get => _smoffset;

            set => _smoffset = (int.TryParse(value, out var ivalue), value?.ToLowerInvariant()) switch
            {
                (false, _) => throw new ArgumentException(Properties.Resources.Between_1_and_12, nameof(SMOffset)),
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException(Properties.Resources.Between_1_and_12, nameof(SMOffset))
            };
        }

        private string _mdoffset;
        [Parameter]
        public string MDOffset
        {
            get => _mdoffset;

            set => _mdoffset = (int.TryParse(value, out var ivalue), value?.ToLowerInvariant()) switch
            {
                (false, _) => throw new ArgumentException(Properties.Resources.Between_1_and_12, nameof(MDOffset)),
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException(Properties.Resources.Between_1_and_12, nameof(MDOffset))
            };
        }

        private string _lgoffset;
        [Parameter]
        public string LGOffset
        {
            get => _lgoffset;

            set => _lgoffset = (int.TryParse(value, out var ivalue), value?.ToLowerInvariant()) switch
            {
                (false, _) => throw new ArgumentException(Properties.Resources.Between_1_and_12, nameof(LGOffset)),
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException(Properties.Resources.Between_1_and_12, nameof(LGOffset))
            };
        }

        private string _xloffset;
        [Parameter]
        public string XLOffset
        {
            get => _xloffset;

            set => _xloffset = (int.TryParse(value, out var ivalue), value?.ToLowerInvariant()) switch
            {
                (false, _) => throw new ArgumentException(Properties.Resources.Between_1_and_12, nameof(XLOffset)),
                (true, var lovalue) when ivalue >= 1 && ivalue <= 12 => lovalue,
                _ => throw new ArgumentException(Properties.Resources.Between_1_and_12, nameof(XLOffset))
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
