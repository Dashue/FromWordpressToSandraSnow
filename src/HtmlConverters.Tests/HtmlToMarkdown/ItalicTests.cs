using Xunit;

namespace HtmlConverters.Tests.HtmlToMarkdown
{
    public class ItalicTests
    {
        private HtmlToMarkdownConverter _converter;

        public ItalicTests()
        {
            _converter = new HtmlToMarkdownConverter();
        }

        [Fact]
        public void Should_convert_italic_from_em()
        {
            Assert.Equal("_Italic_", _converter.Convert("<em>Italic</em>"));
        }

        [Fact]
        public void Should_convert_italic_from_i()
        {
            Assert.Equal("_Italic_", _converter.Convert("<i>Italic</i>"));
        }
    }
}