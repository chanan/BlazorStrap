using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSCard : BootstrapComponentBase
    {
        protected string classname =>
              new CssBuilder()
                  .AddClass(GetClass())
                  .AddClass(Class)
              .Build();

        protected string Tag => CardType switch
        {
            CardType.Image => "img",
            CardType.Title => "h5",
            CardType.Subtitle => "h6",
            CardType.Text => "p",
            CardType.Link => "a",
            CardType.Header => HeadingSize.ToDescriptionString(),
            _ => "div"
        };

        [Parameter] protected CardType CardType { get; set; } = CardType.Card;
        [Parameter] protected VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.None;
        [Parameter] protected Alignment Alignment { get; set; } = Alignment.Left;
        [Parameter] protected HeadingSize HeadingSize { get; set; } = HeadingSize.None;
        [Parameter] protected bool IsInverse { get; set; }
        [Parameter] protected Color Color { get; set; } = Color.None;
        [Parameter] protected bool IsOutline { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }

        private string GetImageVerticalAlignmnet() => this.VerticalAlignment switch
        {
            VerticalAlignment.Top => "-top",
            VerticalAlignment.Bottom => "-bottom",
            _ => null
        };

        private string GetCardAlignmnet() => this.Alignment switch
        {
            Alignment.Center => "text-center",
            Alignment.Right => "text-right",
            _ => null
        };

        private string GetCardColor()
        {
            if (Color == Color.None) { return null; }
            var textColor = Color == Color.Light || IsOutline ? null : "text-white";
            var prefix = IsOutline ? "border" : "bg";
            return $"{textColor} {prefix}-{Color.ToDescriptionString()}";
        }

        private string GetTextColor()
        {
            if (IsOutline) { return null; }
            if (Color == Color.None || Color == Color.Light) { return null; }
            return $"text-{Color.ToDescriptionString()}";
        }

        private string GetClass() => this.CardType switch
        {
            CardType.Body => $"card-body {GetTextColor()}",
            CardType.Card => $"card {GetCardColor()} {GetCardAlignmnet()} {(IsInverse ? "bg-dark text-white" : null)}",
            CardType.Image => $"card-img{GetImageVerticalAlignmnet()}",
            CardType.Title => "card-title",
            CardType.Subtitle => "card-subtitle mb-2 text-muted",
            CardType.Link => "card-link",
            CardType.Header => "card-header",
            CardType.Footer => "card-footer",
            CardType.ImageOverlay => "card-img-overlay",
            CardType.Group => "card-group",
            CardType.Deck => "card-deck",
            CardType.Columns => "card-columns",
            _ => null
        };
    }
}
