using Xunit;

namespace HtmlConverters.Tests.HtmlToMarkdown
{
    public class SpanTests
    {
        private HtmlToMarkdownConverter _converter;

        public SpanTests()
        {
            _converter = new HtmlToMarkdownConverter();
        }

        [Fact]
        public void Should_convert_span()
        {
            Assert.Equal("this is span element", _converter.Convert("<span>this is span element</span>"));
        }

        [Fact]
        public void Should_convert_span_from_string()
        {
            Assert.Equal("before this is span element after", _converter.Convert("before<span>this is span element</span>after"));
        }

        [Fact]
        public void Should_convert_span_from_string_with_space()
        {
            Assert.Equal("before this is span element after", _converter.Convert("before <span>this is span element</span> after"));
        }
    }
}