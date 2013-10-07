using Xunit;

namespace HtmlConverters.Tests.HtmlToMarkdown
{
    public class LinkTests
    {
        private HtmlToMarkdownConverter _converter;

        public LinkTests()
        {
            _converter = new HtmlToMarkdownConverter();
        }

        [Fact]
        public void Should_convert_links()
        {
            var html = "<a href=\"http://www.example.com\" title=\"Example\">Visit Example</a>";
            html += "text1";
            html += "<a href=\"http://www.example1.com\" title=\"Example\">Visit Example1</a>";
            html += "text2";
            html += "<a href=\"http://www.example.com\" title=\"Example\">Visit Example</a>";

            //urls should not be duplicated in reference style
            var expected = "[Visit Example][0]text1[Visit Example1][1]text2[Visit Example][0]";
            expected += "\n\n";
            expected += "[0]: http://www.example.com\n";
            expected += "[1]: http://www.example1.com";

            Assert.Equal(expected, _converter.Convert(html));
        }

        public class InlineTests
        {

            private HtmlToMarkdownConverter _converter;

            public InlineTests()
            {
                _converter = new HtmlToMarkdownConverter(true);
            }

            [Fact]
            public void Should_convert_links_inline_style()
            {
                var html = "<a href=\"http://www.example.com\" title=\"Example\">Visit Example</a>";
                var expected = "[Visit Example](http://www.example.com \"Example\")";

                Assert.Equal(expected, _converter.Convert(html));
            }
        }
    }
}