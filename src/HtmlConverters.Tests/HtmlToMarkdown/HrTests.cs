using Xunit;

namespace HtmlConverters.Tests.HtmlToMarkdown
{
    public class HrTests
    {
        private HtmlToMarkdownConverter _converter;

        public HrTests()
        {
            _converter = new HtmlToMarkdownConverter();
        }

        [Fact]
        public void Should_convert_hr()
        {
            Assert.Equal("- - -\n\n", _converter.Convert("<hr />"));
        }

        [Fact]
        public void Should_convert_hr_inside_text()
        {
            Assert.Equal("this is text before hr\n\n- - -\n\nthis is text after hr", _converter.Convert("this is text before hr<hr/>this is text after hr"));
        }
    }
}