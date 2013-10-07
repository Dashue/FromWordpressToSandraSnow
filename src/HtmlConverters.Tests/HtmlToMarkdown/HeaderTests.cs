using Xunit;

namespace HtmlConverters.Tests.HtmlToMarkdown
{
    public class HeaderTests
    {
        private HtmlToMarkdownConverter _converter;

        public HeaderTests()
        {
            _converter = new HtmlToMarkdownConverter();
        }

        [Fact]
        public void Should_convert_h1()
        {
            Assert.Equal("# H1\n\n", _converter.Convert("<h1>H1</h1>"));
        }

        [Fact]
        public void Should_convert_h2()
        {
            Assert.Equal("## H2\n\n", _converter.Convert("<h2>H2</h2>"));
        }

        [Fact]
        public void Should_convert_h3()
        {
            Assert.Equal("### H3\n\n", _converter.Convert("<h3>H3</h3>"));
        }

        [Fact]
        public void Should_convert_h4()
        {
            Assert.Equal("#### H4\n\n", _converter.Convert("<h4>H4</h4>"));
        }

        [Fact]
        public void Should_convert_h5()
        {
            Assert.Equal("##### H5\n\n", _converter.Convert("<h5>H5</h5>"));
        }
        [Fact]
        public void Should_convert_h6()
        {
            Assert.Equal("###### H6\n\n", _converter.Convert("<h6>H6</h6>"));
        }
    }
}