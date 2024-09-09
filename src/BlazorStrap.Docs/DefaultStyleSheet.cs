using ColorCode;
using ColorCode.Common;
using ColorCode.Styling;

namespace BlazorStrap_Docs
{
    public class DefaultStyleSheet : IStyleSheet
    {
        private static readonly Color DullRed = new Color(163, 21, 21);
        private static readonly StyleDictionary styles;

        static DefaultStyleSheet()
        {
            styles = new StyleDictionary
                         {
                             new Style(ScopeName.PlainText)
                                 {
                                     Foreground = Color.Black,
                                     Background = Color.Empty,
                                     CssClassName = "plainText"
                                 },
                             new Style(ScopeName.HtmlServerSideScript)
                                 {
                                     Background = Color.Yellow,
                                     CssClassName = "htmlServerSideScript"
                                 },
                             new Style(ScopeName.HtmlComment)
                                 {
                                     Foreground = Color.Green,
                                     CssClassName = "htmlComment"
                                 },
                             new Style(ScopeName.HtmlTagDelimiter)
                                 {
                                     Foreground = Color.Blue,
                                     CssClassName = "htmlTagDelimiter"
                                 },
                             new Style(ScopeName.HtmlElementName)
                                 {
                                     Foreground = DullRed,
                                     CssClassName = "htmlElementName"
                                 },
                             new Style(ScopeName.HtmlAttributeName)
                                 {
                                     Foreground = Color.Red,
                                     CssClassName = "htmlAttributeName"
                                 },
                             new Style(ScopeName.HtmlAttributeValue)
                                 {
                                     Foreground = Color.Blue,
                                     CssClassName = "htmlAttributeValue"
                                 },
                             new Style(ScopeName.HtmlOperator)
                                 {
                                     Foreground = Color.Blue,
                                     CssClassName = "htmlOperator"
                                 },
                             new Style(ScopeName.Comment)
                                 {
                                     Foreground = Color.Green,
                                     CssClassName = "comment"
                                 },
                             new Style(ScopeName.XmlDocTag)
                                 {
                                     Foreground = Color.Gray,
                                     CssClassName = "xmlDocTag"
                                 },
                             new Style(ScopeName.XmlDocComment)
                                 {
                                     Foreground = Color.Green,
                                     CssClassName = "xmlDocComment"
                                 },
                             new Style(ScopeName.String)
                                 {
                                     Foreground = DullRed,
                                     CssClassName = "string"
                                 },
                             new Style(ScopeName.StringCSharpVerbatim)
                                 {
                                     Foreground = DullRed,
                                     CssClassName = "stringCSharpVerbatim"
                                 },
                             new Style(ScopeName.Keyword)
                                 {
                                     Foreground = Color.Blue,
                                     CssClassName = "keyword"
                                 },
                             new Style(ScopeName.PreprocessorKeyword)
                                 {
                                     Foreground = Color.Blue,
                                     CssClassName = "preprocessorKeyword"
                                 },
                             new Style(ScopeName.HtmlEntity)
                                 {
                                     Foreground = Color.Red,
                                     CssClassName = "htmlEntity"
                                 },
                             new Style(ScopeName.XmlAttribute)
                                 {
                                     Foreground = Color.Red,
                                     CssClassName = "xmlAttribute"
                                 },
                             new Style(ScopeName.XmlAttributeQuotes)
                                 {
                                     Foreground = Color.Black,
                                     CssClassName = "xmlAttributeQuotes"
                                 },
                             new Style(ScopeName.XmlAttributeValue)
                                 {
                                     Foreground = Color.Blue,
                                     CssClassName = "xmlAttributeValue"
                                 },
                             new Style(ScopeName.XmlCDataSection)
                                 {
                                     Foreground = Color.Gray,
                                     CssClassName = "xmlCDataSection"
                                 },
                             new Style(ScopeName.XmlComment)
                                 {
                                     Foreground = Color.Green,
                                     CssClassName = "xmlComment"
                                 },
                             new Style(ScopeName.XmlDelimiter)
                                 {
                                     Foreground = Color.Blue,
                                     CssClassName = "xmlDelimiter"
                                 },
                             new Style(ScopeName.XmlName)
                                 {
                                     Foreground = DullRed,
                                     CssClassName = "xmlName"
                                 },
                             new Style(ScopeName.ClassName)
                                 {
                                     Foreground = Color.MediumTurquoise,
                                     CssClassName = "className"
                                 },
                             new Style(ScopeName.CssSelector)
                                 {
                                     Foreground = DullRed,
                                     CssClassName = "cssSelector"
                                 },
                             new Style(ScopeName.CssPropertyName)
                                 {
                                     Foreground = Color.Red,
                                     CssClassName = "cssPropertyName"
                                 },
                             new Style(ScopeName.CssPropertyValue)
                                 {
                                     Foreground = Color.Blue,
                                     CssClassName = "cssPropertyValue"
                                 },
                             new Style(ScopeName.SqlSystemFunction)
                                 {
                                     Foreground = Color.Magenta,
                                     CssClassName = "sqlSystemFunction"
                                 },
                             new Style(ScopeName.PowerShellAttribute)
                                 {
                                     Foreground = Color.PowderBlue,
                                     CssClassName = "powershellAttribute"
                                 },
                             new Style(ScopeName.PowerShellOperator)
                                 {
                                     Foreground = Color.Gray,
                                     CssClassName = "powershellOperator"
                                 },
                             new Style(ScopeName.PowerShellType)
                                 {
                                     Foreground = Color.Teal,
                                     CssClassName = "powershellType"
                                 },
                             new Style(ScopeName.PowerShellVariable)
                                 {
                                     Foreground = Color.OrangeRed,
                                     CssClassName = "powershellVariable"
                                 },

                             new Style(ScopeName.Type)
                                {
                                    Foreground = Color.Teal,
                                    CssClassName = "type"
                                },
                             new Style(ScopeName.TypeVariable)
                                {
                                    Foreground = Color.Teal,
                                    Italic = true,
                                    CssClassName = "typeVariable"
                                },
                             new Style(ScopeName.NameSpace)
                                {
                                    Foreground = Color.Navy,
                                    CssClassName = "namespace"
                                },
                             new Style(ScopeName.Constructor)
                                {
                                    Foreground = Color.Purple,
                                    CssClassName = "constructor"
                                },
                             new Style(ScopeName.Predefined)
                                {
                                    Foreground = Color.Navy,
                                    CssClassName = "predefined"
                                },
                             new Style(ScopeName.PseudoKeyword)
                                {
                                    Foreground = Color.Navy,
                                    CssClassName = "pseudoKeyword"
                                },
                             new Style(ScopeName.StringEscape)
                                {
                                    Foreground = Color.Gray,
                                    CssClassName = "stringEscape"
                                },
                             new Style(ScopeName.ControlKeyword)
                                {
                                    Foreground = Color.Blue,
                                    CssClassName = "controlKeyword"
                                },
                             new Style(ScopeName.Number)
                                {
                                    CssClassName = "number"
                                },
                             new Style(ScopeName.Operator)
                                {
                                    CssClassName = "operator"
                                },
                             new Style(ScopeName.Delimiter)
                                {
                                    CssClassName = "delimiter"
                                },

                             new Style(ScopeName.MarkdownHeader)
                                {
                                    // Foreground = Color.Bluelue,
                                    Bold = true,
                                    CssClassName = "markdownHeader"
                                },
                             new Style(ScopeName.MarkdownCode)
                                {
                                    Foreground = Color.Teal,
                                    CssClassName = "markdownCode"
                                },
                             new Style(ScopeName.MarkdownListItem)
                                {
                                    Bold = true,
                                    CssClassName = "markdownListItem"
                                },
                             new Style(ScopeName.MarkdownEmph)
                                {
                                    Italic = true,
                                    CssClassName = "italic"
                                },
                             new Style(ScopeName.MarkdownBold)
                                {
                                    Bold = true,
                                    CssClassName = "bold"
                                },
                         };
        }

        public string Name
        {
            get { return "DefaultStyleSheet"; }
        }

        public StyleDictionary Styles
        {
            get { return styles; }
        }
    }
}