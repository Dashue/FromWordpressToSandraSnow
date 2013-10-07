using Xunit;

namespace HtmlConverters.Tests.HtmlToMarkdown
{
    public class TitleTests
    {
        private HtmlToMarkdownConverter _converter;

        public TitleTests()
        {
            _converter = new HtmlToMarkdownConverter();
        }

        [Fact]
        public void Should_convert_title()
        {
            Assert.Equal("# This is document Title\n\n", _converter.Convert("<title>This is document Title</title>"));
        }
    }
}