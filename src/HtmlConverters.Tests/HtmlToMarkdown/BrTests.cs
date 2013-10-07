using Xunit;

namespace HtmlConverters.Tests.HtmlToMarkdown
{
    public class BrTests
    {
        private HtmlToMarkdownConverter _converter;

        public BrTests()
        {
            _converter = new HtmlToMarkdownConverter();
        }

        [Fact]
        public void Should_convert_br()
        {
            Assert.Equal("  \n", _converter.Convert("<br/>"));
        }

        [Fact]
        public void Should_convert_br_inside_text()
        {
            Assert.Equal("this is text before break  \nthis is text after break",
                         _converter.Convert("this is text before break<br/>this is text after break"));
        }
    }
}