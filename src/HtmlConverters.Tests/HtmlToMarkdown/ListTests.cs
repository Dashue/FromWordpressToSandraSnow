using Xunit;

namespace HtmlConverters.Tests.HtmlToMarkdown
{
    public class ListTests
    {
        private HtmlToMarkdownConverter _converter;

        public ListTests()
        {
            _converter = new HtmlToMarkdownConverter();
        }

        [Fact]
        public void Should_convert_ul()
        {
            Assert.Equal("* item a\n* item b\n", _converter.Convert("<ul><li>item a</li><li>item b</li></ul>"));
        }

        [Fact]
        public void Should_convert_ul_nested()
        {
            string html = "<ul><li>item a<ul><li>item aa</li><li>item bb</li></ul></li><li>item b</li></ul>";
            var expected = "* item a\n  * item aa\n  * item bb\n* item b\n";

            Assert.Equal(expected, _converter.Convert(html));
        }

        [Fact]
        public void Should_convert_ul_empty()
        {
            Assert.Equal("* item 1\n", _converter.Convert("<ul><li>item 1</li><li></li></ol>"));
        }

        [Fact]
        public void Should_convert_ol()
        {
            Assert.Equal("1. item 1\n2. item 2", _converter.Convert("<ol><li>item 1</li><li>item 2</li></ol>"));
        }

        [Fact]
        public void Should_convert_ol_nested()
        {
            string html = "<ul><li>item a<ul><li>item aa</li><li>item bb</li></ul></li><li>item b</li></ul>";
            var expected = "1 item a\n  1 item aa\n  2 item bb\n\n2 item b\n";

            Assert.Equal(expected, _converter.Convert(html));
        }

        [Fact]
        public void Should_convert_ol_empty()
        {
            Assert.Equal("1. item 1\n", _converter.Convert("<ol><li>item 1</li><li/></ol>"));
        }
    }
}