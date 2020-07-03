using System.Threading.Tasks;
using Bunit;
using FluentAssertions;
using Xunit;

namespace BlazorStrap.Tests.Components.Forms.Button
{
    public class Button_Color_ShouldChangeItsColor : TestBase
    {
        [Theory]
        [InlineData(Color.Primary, Color.Secondary, false, true, false, false, false, false, false, false, false)]
        [InlineData(Color.Primary, Color.Success, false, false, true, false, false, false, false, false, false)]
        [InlineData(Color.Primary, Color.Info, false, false, false, true, false, false, false, false, false)]
        [InlineData(Color.Primary, Color.Warning, false, false, false, false, true, false, false, false, false)]
        [InlineData(Color.Primary, Color.Danger, false, false, false, false, false, true, false, false, false)]
        [InlineData(Color.Primary, Color.Light, false, false, false, false, false, false, true, false, false)]
        [InlineData(Color.Primary, Color.Dark, false, false, false, false, false, false, false, true, false)]
        [InlineData(Color.Primary, Color.Link, false, false, false, false, false, false, false, false, true)]
        [InlineData(Color.Secondary, Color.Success, false, false, true, false, false, false, false, false, false)]
        [InlineData(Color.Secondary, Color.Info, false, false, false, true, false, false, false, false, false)]
        [InlineData(Color.Secondary, Color.Warning, false, false, false, false, true, false, false, false, false)]
        [InlineData(Color.Secondary, Color.Danger, false, false, false, false, false, true, false, false, false)]
        [InlineData(Color.Secondary, Color.Light, false, false, false, false, false, false, true, false, false)]
        [InlineData(Color.Secondary, Color.Dark, false, false, false, false, false, false, false, true, false)]
        [InlineData(Color.Secondary, Color.Link, false, false, false, false, false, false, false, false, true)]
        [InlineData(Color.Secondary, Color.Primary, true, false, false, false, false, false, false, false, false)]
        [InlineData(Color.Success, Color.Info, false, false, false, true, false, false, false, false, false)]
        [InlineData(Color.Success, Color.Warning, false, false, false, false, true, false, false, false, false)]
        [InlineData(Color.Success, Color.Danger, false, false, false, false, false, true, false, false, false)]
        [InlineData(Color.Success, Color.Light, false, false, false, false, false, false, true, false, false)]
        [InlineData(Color.Success, Color.Dark, false, false, false, false, false, false, false, true, false)]
        [InlineData(Color.Success, Color.Link, false, false, false, false, false, false, false, false, true)]
        [InlineData(Color.Success, Color.Primary, true, false, false, false, false, false, false, false, false)]
        [InlineData(Color.Success, Color.Secondary, false, true, false, false, false, false, false, false, false)]
        [InlineData(Color.Info, Color.Warning, false, false, false, false, true, false, false, false, false)]
        [InlineData(Color.Info, Color.Danger, false, false, false, false, false, true, false, false, false)]
        [InlineData(Color.Info, Color.Light, false, false, false, false, false, false, true, false, false)]
        [InlineData(Color.Info, Color.Dark, false, false, false, false, false, false, false, true, false)]
        [InlineData(Color.Info, Color.Link, false, false, false, false, false, false, false, false, true)]
        [InlineData(Color.Info, Color.Primary, true, false, false, false, false, false, false, false, false)]
        [InlineData(Color.Info, Color.Secondary, false, true, false, false, false, false, false, false, false)]
        [InlineData(Color.Info, Color.Success, false, false, true, false, false, false, false, false, false)]
        [InlineData(Color.Warning, Color.Danger, false, false, false, false, false, true, false, false, false)]
        [InlineData(Color.Warning, Color.Light, false, false, false, false, false, false, true, false, false)]
        [InlineData(Color.Warning, Color.Dark, false, false, false, false, false, false, false, true, false)]
        [InlineData(Color.Warning, Color.Link, false, false, false, false, false, false, false, false, true)]
        [InlineData(Color.Warning, Color.Primary, true, false, false, false, false, false, false, false, false)]
        [InlineData(Color.Warning, Color.Secondary, false, true, false, false, false, false, false, false, false)]
        [InlineData(Color.Warning, Color.Success, false, false, true, false, false, false, false, false, false)]
        [InlineData(Color.Warning, Color.Info, false, false, false, true, false, false, false, false, false)]
        [InlineData(Color.Danger, Color.Light, false, false, false, false, false, false, true, false, false)]
        [InlineData(Color.Danger, Color.Dark, false, false, false, false, false, false, false, true, false)]
        [InlineData(Color.Danger, Color.Link, false, false, false, false, false, false, false, false, true)]
        [InlineData(Color.Danger, Color.Primary, true, false, false, false, false, false, false, false, false)]
        [InlineData(Color.Danger, Color.Secondary, false, true, false, false, false, false, false, false, false)]
        [InlineData(Color.Danger, Color.Success, false, false, true, false, false, false, false, false, false)]
        [InlineData(Color.Danger, Color.Info, false, false, false, true, false, false, false, false, false)]
        [InlineData(Color.Danger, Color.Warning, false, false, false, false, true, false, false, false, false)]
        [InlineData(Color.Light, Color.Dark, false, false, false, false, false, false, false, true, false)]
        [InlineData(Color.Light, Color.Link, false, false, false, false, false, false, false, false, true)]
        [InlineData(Color.Light, Color.Primary, true, false, false, false, false, false, false, false, false)]
        [InlineData(Color.Light, Color.Secondary, false, true, false, false, false, false, false, false, false)]
        [InlineData(Color.Light, Color.Success, false, false, true, false, false, false, false, false, false)]
        [InlineData(Color.Light, Color.Info, false, false, false, true, false, false, false, false, false)]
        [InlineData(Color.Light, Color.Warning, false, false, false, false, true, false, false, false, false)]
        [InlineData(Color.Light, Color.Danger, false, false, false, false, false, true, false, false, false)]
        [InlineData(Color.Dark, Color.Link, false, false, false, false, false, false, false, false, true)]
        [InlineData(Color.Dark, Color.Primary, true, false, false, false, false, false, false, false, false)]
        [InlineData(Color.Dark, Color.Secondary, false, true, false, false, false, false, false, false, false)]
        [InlineData(Color.Dark, Color.Success, false, false, true, false, false, false, false, false, false)]
        [InlineData(Color.Dark, Color.Info, false, false, false, true, false, false, false, false, false)]
        [InlineData(Color.Dark, Color.Warning, false, false, false, false, true, false, false, false, false)]
        [InlineData(Color.Dark, Color.Light, false, false, false, false, false, false, true, false, false)]
        [InlineData(Color.Light, Color.Primary, true, false, false, false, false, false, false, false, false)]
        [InlineData(Color.Light, Color.Secondary, false, true, false, false, false, false, false, false, false)]
        [InlineData(Color.Light, Color.Success, false, false, true, false, false, false, false, false, false)]
        [InlineData(Color.Light, Color.Info, false, false, false, true, false, false, false, false, false)]
        [InlineData(Color.Light, Color.Warning, false, false, false, false, true, false, false, false, false)]
        [InlineData(Color.Light, Color.Danger, false, false, false, false, false, true, false, false, false)]
        [InlineData(Color.Light, Color.Light, false, false, false, false, false, false, true, false, false)]
        [InlineData(Color.Light, Color.Dark, false, false, false, false, false, false, false, true, false)]
        public async Task ToTheNewColor(
            Color startColor,
            Color targetColor,
            bool isPrimary,
            bool isSecondary,
            bool isSuccess,
            bool isInfo,
            bool isWarning,
            bool isDanger,
            bool isLight,
            bool isDark,
            bool isLink)
        {
            // Arrange
            IRenderedComponent<BSButton> cut = RenderComponent<BSButton>(
                (ButtonApi.Color, startColor));

            // Act
            cut.SetParametersAndRender(prm => prm.Add(p => p.Color, targetColor));

            // Assert
            cut.Find(HtmlTags.Button);
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.Btn);
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.BtnPrimary).Should().Be(isPrimary);
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.BtnSecondary).Should().Be(isSecondary);
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.BtnSuccess).Should().Be(isSuccess);
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.BtnInfo).Should().Be(isInfo);
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.BtnWarning).Should().Be(isWarning);
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.BtnDanger).Should().Be(isDanger);
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.BtnLight).Should().Be(isLight);
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.BtnDark).Should().Be(isDark);
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.BtnLink).Should().Be(isLink);
        }
    }
}