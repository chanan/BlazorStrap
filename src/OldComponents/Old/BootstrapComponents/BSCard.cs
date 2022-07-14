using BlazorComponentUtilities;
using BlazorStrap.Components.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap
{
    public class BSCard : LayoutBase
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

        private string? ClassBuilder => new CssBuilder()
            .AddClass(GetClass())
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private string Tag => CardType switch
        {
            CardType.Image => "img",
            CardType.Title => "h5",
            CardType.Subtitle => "h6",
            CardType.Text => "p",
            CardType.Link => "a",
            CardType.Header => HeadingSize.ToDescriptionString(),
            _ => "div"
        };

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Attributes.TryGetValue("src", out var value) && (value.ToString()?.Contains("placeholder:") ?? false) && CardType == CardType.Image)
            {
                builder.OpenComponent(0, typeof(BSImage));
                builder.AddAttribute(1, "Class", ClassBuilder);
                builder.AddAttribute(2, "IsPlaceholder", true);
                builder.AddAttribute(3, "Source", value.ToString()?.Replace("placeholder:", ""));
                builder.AddMultipleAttributes(4, Attributes);
                builder.CloseComponent();
            }
            else
            {
                builder.OpenElement(0, Tag);
                builder.AddAttribute(1, "class", ClassBuilder);
                builder.AddMultipleAttributes(2, Attributes);
                builder.AddContent(3, ChildContent);
                builder.CloseElement();
            }

        }

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
            if (Color == BSColor.Default) { return null; }
            var textColor = Color == BSColor.Light || IsOutline ? null : "text-white";
            var prefix = IsOutline ? "border" : "bg";
            return $"{textColor} {prefix}-{Color.NameToLower()}";
        }

        private string? GetClass()
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
    }
}