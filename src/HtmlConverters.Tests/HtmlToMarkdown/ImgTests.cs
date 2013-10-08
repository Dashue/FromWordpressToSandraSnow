using Xunit;

namespace HtmlConverters.Tests.HtmlToMarkdown
{
    public class ImgTests
    {
        private HtmlToMarkdownConverter _converter;

        public ImgTests()
        {
            _converter = new HtmlToMarkdownConverter();
        }

        [Fact]
        public void Should_convert_images_from_reference_style()
        {
            var html = "<img alt=\"Example Image\" title=\"Free example image\" src=\"/img/62838.jpg\"/>";
            var expected = "![Example Image][0]\n\n[0]: /img/62838.jpg";

            Assert.Equal(expected, _converter.Convert(html));
        }

        [Fact]
        public void Should_convert_images_from_text()
        {
            var html = "before<img alt=\"Example Image\" title=\"Free example image\" src=\"/img/62838.jpg\"/>after";
            var expected = "before\n\n![Example Image][0]\n\nafter\n\n[0]: /img/62838.jpg";

            Assert.Equal(expected, _converter.Convert(html));
        }

        [Fact]
        public void Should_convert_images_from_reference_style_if_alt_empty_use_title()
        {
            var html = "<img title=\"Free example image title\" src=\"/img/62838.jpg\">";
            var expected = "![Free example image title][0]\n\n[0]: /img/62838.jpg";

            Assert.Equal(expected, _converter.Convert(html));
        }

        [Fact]
        public void Should_convert_image_wrapped_in_anchor_to_markdown()
        {
            var html = "<a href='/exec/j/4/?pid=62838&lno=1&afsrc=1'><img alt='Example Image' title='Free example image' src='/img/62838.jpg'></a>";
            var expected = "[![Example Image](/img/62838.jpg \"Free example image\")](/exec/j/4/?pid=62838&lno=1&afsrc=1)";

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
            public void Should_convert_image_wrapped_in_anchor_to_markdown()
            {
                var html = "<a href=\"/exec/j/4/?pid=62838&lno=1&afsrc=1\"><img alt=\"Example Image\" title=\"Free example image\" src=\"/img/62838.jpg\"></a>";
                var expected = "[![Example Image](/img/62838.jpg \"Free example image\")](/exec/j/4/?pid=62838&lno=1&afsrc=1)";

                Assert.Equal(expected, _converter.Convert(html));
            }

            [Fact]
            public void Should_convert_images_inline()
            {
                var html = "<img alt=\"Example Image\" title=\"Free example image\" src=\"/img/62838.jpg\"/>";
                var expected = "![Example Image](/img/62838.jpg \"Free example image\")\n\n";

                Assert.Equal(expected, _converter.Convert(html));
            }

            [Fact]
            public void Should_convert_images_from_text()
            {
                var html = "before<img alt=\"Example Image\" title=\"Free example image\" src=\"/img/62838.jpg\"/>after";
                var expected = "before\n\n![Example Image](/img/62838.jpg \"Free example image\")\n\nafter";

                Assert.Equal(expected, _converter.Convert(html));
            }
        }
    }
}