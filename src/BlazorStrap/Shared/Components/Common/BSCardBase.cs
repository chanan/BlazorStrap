using BlazorStrap.Extensions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSCardBase : BlazorStrapBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Parameter] public Alignment Alignment { get; set; } = Alignment.Left;

        /// <summary>
        /// Sets card type.
        /// </summary>
        [Parameter] public CardType CardType { get; set; } = CardType.Card;

        /// <summary>
        /// Sets the card color.
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        /// <summary>
        /// Sets card heading size.
        /// </summary>
        [Parameter] public HeadingSize HeadingSize { get; set; } = HeadingSize.None;

        /// <summary>
        /// Inverts color scheme.
        /// </summary>
        [Parameter] public bool IsInverse { get; set; }

        /// <summary>
        /// Sets styling to be outlined.
        /// </summary>
        [Parameter] public bool IsOutline { get; set; }

        /// <summary>
        /// Sets image to be on top or bottom of card. Only used if <see cref="CardType"/> is <see cref="CardType.Image"/>
        /// </summary>
        [Parameter] public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.None;

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
        private string? GetCardAlignment()
        {
            return Alignment switch
            {
                Alignment.Center => "text-center",
                Alignment.Right => "text-right",
                _ => null
            };
        }

        private string? GetCardColor()
        {
            if (Color == BSColor.Default) return null;             var textColor = Color == BSColor.Light || IsOutline ? null : "text-white";
            var prefix = IsOutline ? "border" : "bg";
            return $"{textColor} {prefix}-{Color.NameToLower()}";
        }
        protected string? GetClass()
        {
            return CardType switch
            {
                CardType.Body => $"card-body {GetTextColor()}",
                CardType.Card => $"card {GetCardColor()} {GetCardAlignment()} {(IsInverse ? "bg-dark text-white" : null)}",
                CardType.Image => $"card-img{GetImageVerticalAlignment()}",
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
        private string? GetImageVerticalAlignment()
        {
            return VerticalAlignment switch
            {
                VerticalAlignment.Top => "-top",
                VerticalAlignment.Bottom => "-bottom",
                _ => null
            };
        }

        private string? GetTextColor()
        {
            return IsOutline ? null : Color == BSColor.Default || Color == BSColor.Light ? null : $"text-{Color.ToDescriptionString()}";
        }

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
    }
}
