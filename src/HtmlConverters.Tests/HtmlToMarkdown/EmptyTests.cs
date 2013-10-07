using Xunit;

namespace HtmlConverters.Tests.HtmlToMarkdown
{
    public class EmptyTests
    {
        private HtmlToMarkdownConverter _converter;

        public EmptyTests()
        {
            _converter = new HtmlToMarkdownConverter();
        }

        [Fact]
        public void Should_not_convert_empty_div()
        {
            Assert.Equal(string.Empty, _converter.Convert("<div>        </div>"));
        }

        [Fact]
        public void Should_not_convert_empty_h1()
        {
            Assert.Equal(string.Empty, _converter.Convert("<h1>        </h1>"));
        }

        [Fact]
        public void Should_not_convert_empty_b()
        {
            Assert.Equal(string.Empty, _converter.Convert("<b>        </b>"));
        }

        [Fact]
        public void Should_not_convert_images_without_url()
        {
            Assert.Equal(string.Empty, _converter.Convert("<img alt=\"Example Image\" title=\"Free example image\">"));
        }

        [Fact]
        public void Should_not_convert_link_without_text()
        {
            Assert.Equal(string.Empty, _converter.Convert("<a href='/'/>"));
        }
    }
}