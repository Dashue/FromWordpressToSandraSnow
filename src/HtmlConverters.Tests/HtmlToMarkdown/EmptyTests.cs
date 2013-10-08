using Xunit;

namespace HtmlConverters.Tests.HtmlToMarkdown
{
    public class EmptyTests
    {
        private HtmlToMarkdownConverter _converter;

        public EmptyTests()
        {
            _converter = new HtmlToMarkdownConverter();
        }

        [Fact]
        public void Should_not_convert_empty_div()
        {
            Assert.Equal(string.Empty, _converter.Convert("<div>        </div>"));
        }

        [Fact]
        public void Should_not_convert_empty_h1()
        {
            Assert.Equal(string.Empty, _converter.Convert("<h1>        </h1>"));
        }

        [Fact]
        public void Should_not_convert_empty_b()
        {
            Assert.Equal(string.Empty, _converter.Convert("<b>        </b>"));
        }

        [Fact]
        public void Should_not_convert_images_without_url()
        {
            Assert.Equal(string.Empty, _converter.Convert("<img alt=\"Example Image\" title=\"Free example image\">"));
        }

        [Fact]
        public void Should_not_convert_link_without_text()
        {
            Assert.Equal(string.Empty, _converter.Convert("<a href='/'/>"));
        }

        [Fact]
        public void Should_not_convert_head()
        {
            var html = "<head>asd</head>";
            Assert.Equal(string.Empty, _converter.Convert(html));
        }

        [Fact]
        public void Should_not_convert_link()
        {
            var html = "<link>asd</link>";
            Assert.Equal(string.Empty, _converter.Convert(html));
        }

        [Fact]
        public void Should_not_convert_script()
        {
            var html = "<script>asd</script>";
            Assert.Equal(string.Empty, _converter.Convert(html));
        }

        [Fact]
        public void Should_not_convert_style()
        {
            var html = "<style>asd</style>";
            Assert.Equal(string.Empty, _converter.Convert(html));
        }

        [Fact]
        public void Should_not_convert_option()
        {
            var html = "<option>asd</option>";
            Assert.Equal(string.Empty, _converter.Convert(html));
        }

        [Fact]
        public void Should_not_convert_noscript()
        {
            var html = "<noscript>asd</noscript>";
            Assert.Equal(string.Empty, _converter.Convert(html));
        }

        [Fact]
        public void Should_not_convert_noframes()
        {
            var html = "<noframes>asd</noframes>";
            Assert.Equal(string.Empty, _converter.Convert(html));
        }
        [Fact]
        public void Should_not_convert_input()
        {
            var html = "<input>asd</input>";
            Assert.Equal(string.Empty, _converter.Convert(html));
        }
        [Fact]
        public void Should_not_convert_button()
        {
            var html = "<button>asd</button>";
            Assert.Equal(string.Empty, _converter.Convert(html));
        }
        [Fact]
        public void Should_not_convert_select()
        {
            var html = "<select>asd</select>";
            Assert.Equal(string.Empty, _converter.Convert(html));
        }
        [Fact]
        public void Should_not_convert_textarea()
        {
            var html = "<textarea>asd</textarea>";
            Assert.Equal(string.Empty, _converter.Convert(html));
        }
        [Fact]
        public void Should_not_convert_label()
        {
            var html = "<label>asd</label>";
            Assert.Equal(string.Empty, _converter.Convert(html));
        }
    }
}