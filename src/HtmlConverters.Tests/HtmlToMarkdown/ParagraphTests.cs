using Xunit;

namespace HtmlConverters.Tests.HtmlToMarkdown
{
    public class ParagraphTests
    {
        private HtmlToMarkdownConverter _converter;

        public ParagraphTests()
        {
            _converter = new HtmlToMarkdownConverter();
        }

        [Fact]
        public void Should_convert_paragraph()
        {
            Assert.Equal("This is a paragraph. This is the second sentence.\n\n", _converter.Convert("<p>This is a paragraph. This is the second sentence.</p>"));
        }
        [Fact]
        public void Should_convert_paragraph_inside_text()
        {
            Assert.Equal("this is text before paragraph\n\nThis is a paragraph\n\nthis is text after paragraph",
                         _converter.Convert(
                             "this is text before paragraph<p>This is a paragraph</p>this is text after paragraph"));
        }
    }
}