using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Bunit;
using FluentAssertions;
using Xunit;

namespace BlazorStrap.Tests.Components.Forms.Button
{
    [ExcludeFromCodeCoverage]
    public class Button_Color_ShouldBe : TestBase
    {
        [Fact]
        public async Task Primary_WhenNoColorIsSetUp()
        {
            // Arrange
            IRenderedComponent<BSButton> cut = RenderComponent<BSButton>();

            // Assert
            cut.Find(HtmlTags.Button);
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.Btn).Should().BeTrue();
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.BtnPrimary).Should().BeTrue();
        }

        [Fact]
        public async Task Primary_WhenItConfiguredToBePrimary()
        {
            // Arrange
            IRenderedComponent<BSButton> cut = RenderComponent<BSButton>(
                (ButtonApi.Color, Color.Primary));

            // Assert
            cut.Find(HtmlTags.Button);
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.Btn).Should().BeTrue();
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.BtnPrimary).Should().BeTrue();
        }

        [Fact]
        public async Task Secondary_WhenColorConfiguredToSecondary()
        {
            // Arrange
            IRenderedComponent<BSButton> cut = RenderComponent<BSButton>(
                (ButtonApi.Color, Color.Secondary));

            // Assert
            cut.Find(HtmlTags.Button);
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.Btn).Should().BeTrue();
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.BtnSecondary).Should().BeTrue();
        }

        [Fact]
        public async Task Success_WhenColorConfiguredToSuccess()
        {
            // Arrange
            IRenderedComponent<BSButton> cut = RenderComponent<BSButton>(
                (ButtonApi.Color, Color.Success));

            // Assert
            cut.Find(HtmlTags.Button);
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.Btn).Should().BeTrue();
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.BtnSuccess).Should().BeTrue();
        }

        [Fact]
        public async Task Info_WhenColorConfiguredToInfo()
        {
            // Arrange
            IRenderedComponent<BSButton> cut = RenderComponent<BSButton>(
                (ButtonApi.Color, Color.Info));

            // Assert
            cut.Find(HtmlTags.Button);
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.Btn).Should().BeTrue();
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.BtnInfo).Should().BeTrue();
        }

        [Fact]
        public async Task Warning_WhenColorConfiguredToWarning()
        {
            // Arrange
            IRenderedComponent<BSButton> cut = RenderComponent<BSButton>(
                (ButtonApi.Color, Color.Warning));

            // Assert
            cut.Find(HtmlTags.Button);
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.Btn).Should().BeTrue();
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.BtnWarning).Should().BeTrue();
        }

        [Fact]
        public async Task Danger_WhenColorConfiguredToDanger()
        {
            // Arrange
            IRenderedComponent<BSButton> cut = RenderComponent<BSButton>(
                (ButtonApi.Color, Color.Danger));

            // Assert
            cut.Find(HtmlTags.Button);
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.Btn).Should().BeTrue();
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.BtnDanger).Should().BeTrue();
        }

        [Fact]
        public async Task Light_WhenColorConfiguredToLight()
        {
            // Arrange
            IRenderedComponent<BSButton> cut = RenderComponent<BSButton>(
                (ButtonApi.Color, Color.Light));

            // Assert
            cut.Find(HtmlTags.Button);
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.Btn).Should().BeTrue();
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.BtnLight).Should().BeTrue();
        }

        [Fact]
        public async Task Dark_WhenItColorConfiguredToDark()
        {
            // Arrange
            IRenderedComponent<BSButton> cut = RenderComponent<BSButton>(
                (ButtonApi.Color, Color.Dark));

            // Assert
            cut.Find(HtmlTags.Button);
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.Btn).Should().BeTrue();
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.BtnDark).Should().BeTrue();
        }

        [Fact]
        public async Task Link_WhenItConfiguredToBePrimary()
        {
            // Arrange
            IRenderedComponent<BSButton> cut = RenderComponent<BSButton>(
                (ButtonApi.Color, Color.Link));

            // Assert
            cut.Find(HtmlTags.Button);
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.Btn).Should().BeTrue();
            cut.Find(HtmlTags.Button).ToMarkup().Contains(ButtonCss.BtnLink).Should().BeTrue();
        }
    }
}