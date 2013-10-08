using Xunit;

namespace HtmlConverters.Tests.HtmlToMarkdown
{
    public class PreTests
    {

        private readonly HtmlToMarkdownConverter _converter;

        public PreTests()
        {
            _converter = new HtmlToMarkdownConverter();
        }

        [Fact]
        public void Should_convert_pre_block()
        {
            var html = "<pre>";
            html += "\tvoid main(String[] args) {\n";
            html += "\t\tSystem.out.println(\"Hello Markdown\");\n";
            html += "\t}";
            html += "</pre>";

            var expected = "\tvoid main(String[] args) {\n";
            expected += "\t\tSystem.out.println(\"Hello Markdown\");\n";
            expected += "\t}";
            expected += "\n\n";

            Assert.Equal(expected, _converter.Convert(html));
        }

        [Fact]
        public void Should_convert_pre_block_with_html()
        {
            var html = "<pre>\n";
            html += "<div a=\"b\">\n";
            html += "\t<span>this is span inside pre block</span>\n";
            html += "\tthis is paragraph inside pre block\n";
            html += "</div>";
            html += "</pre>";

            var expected = "\t\t" + "\n\n\n";
            expected += "\t\t\tthis is span inside pre block\n";
            expected += "\t\t\tthis is paragraph inside pre block\n";
            expected += "    " + "\n";
            expected += "\n";

            /*
             *         
            this is span inside pre block
            this is paragraph inside pre block
        */

            Assert.Equal(expected, _converter.Convert(html));
        }

        //        it("should be able to convert <pre><code>...</code></pre> blocks", function() {
        //            var html= "<pre><code>{% blockquote [author[, source]] [link] [source_link_title] %}";
        //            html+= "\nQuote string";
        //            html+= "\n{% endblockquote %}";
        //            html+= "\n</code></pre>";

        //            var md = markdown(html);
        //            expected="    {% blockquote [author[, source]] [link] [source_link_title] %}";
        //            expected+="\n    Quote string";
        //            expected+="\n    {% endblockquote %}";
        //            expected+="\n    ";
        //            expected+="\n\n";

        //            expect(md).toEqual(expected);
        //        });
    }
}