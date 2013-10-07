using Xunit;

namespace HtmlConverters.Tests.HtmlToMarkdown
{
    public class BoldTests
    {
        private HtmlToMarkdownConverter _converter;

        public BoldTests()
        {
            _converter = new HtmlToMarkdownConverter();
        }

        [Fact]
        public void Should_convert_bold_from_strong()
        {
            Assert.Equal("**Bold**", _converter.Convert("<strong>Bold</strong>"));
        }
        [Fact]
        public void Should_convert_bold_from_b()
        {
            Assert.Equal("**Bold**", _converter.Convert("<b>Bold</b>"));
        }

        [Fact]
        public void Should_convert_strong_inside_text()
        {
            Assert.Equal("This has a **block** word", _converter.Convert("This has a <strong>block</strong> word"));
        }
    }
}